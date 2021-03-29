using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Cs.Application.Buildings.Models;
using Cs.Application.Interfaces;
using Cs.Application.Tools;
using Cs.Common.Models;
using Cs.Domain.Buildings;
using Microsoft.EntityFrameworkCore;

namespace Cs.Application.Buildings
{
    public class WorkBuildingMaterialService : IDependedCrudOperation<WorkBuildingMaterialViewModel>
    {
        private readonly IBuildingDbService _db;
        private readonly IMaterialDbService _materialDb;
        private readonly IMapper _mapper;

        public WorkBuildingMaterialService(IBuildingDbService db, IMapper mapper, IMaterialDbService materialDb)
        {
            _db = db;
            _mapper = mapper;
            _materialDb = materialDb;
        }

        public ListResult<WorkBuildingMaterialViewModel> Get(int workId, Paging paging)
        {
            var work = _db.Works.All()
                .Include(w => w.BuildingMaterials)
                .ThenInclude(bm => bm.BuildingMaterial)
                .ThenInclude(bm => bm.Category)
                .Include(w => w.BuildingMaterials)
                .ThenInclude(bm => bm.BuildingMaterial)
                .ThenInclude(bm => bm.Dimension)
                .FirstOrDefault(w => w.Id == workId);
            if (work == null)
                throw new ServiceException(UiConstants.ObjectNotFound);

            var buildingMaterials = work.BuildingMaterials;
            if (!string.IsNullOrEmpty(paging.Search))
                buildingMaterials = buildingMaterials.Where(t => t.BuildingMaterial.Name.Contains(paging.Search))
                    .ToList();

            var result = buildingMaterials.Skip(paging.Skip).Take(paging.Count).ToList();
            var total = buildingMaterials.Count();

            var viewModels = _mapper.Map<List<WorkBuildingMaterialViewModel>>(result);

            return new ListResult<WorkBuildingMaterialViewModel>(viewModels, total);
        }

        public WorkBuildingMaterialViewModel Get(int workId, int id)
        {
            var work = _db.Works.All()
                .Include(w => w.BuildingMaterials)
                .ThenInclude(bm => bm.BuildingMaterial)
                .ThenInclude(bm => bm.Category)
                .Include(w => w.BuildingMaterials)
                .ThenInclude(bm => bm.BuildingMaterial)
                .ThenInclude(bm => bm.Dimension)
                .FirstOrDefault(w => w.Id == workId);

            if (work == null)
                throw new ServiceException(UiConstants.ObjectNotFound);

            var buildingMaterial = work.BuildingMaterials.FirstOrDefault(bm => bm.MaterialId == id);
            if (buildingMaterial == null)
                throw new ServiceException(UiConstants.ObjectNotFound);

            return _mapper.Map<WorkBuildingMaterialViewModel>(buildingMaterial);
        }

        public int Add(int workId, WorkBuildingMaterialViewModel t)
        {
            var work = _db.Works.All()
                .Include(w => w.BuildingMaterials)
                .ThenInclude(bm => bm.BuildingMaterial)
                .FirstOrDefault(w => w.Id == workId);

            if (work == null)
                throw new ServiceException(UiConstants.ObjectNotFound);

            var dbBuildingMaterial = work.BuildingMaterials.FirstOrDefault(bm => bm.MaterialId == t.MaterialId);
            if (dbBuildingMaterial == null)
            {
                var workBuildingMaterial = _mapper.Map<WorkBuildingMaterial>(t);
                workBuildingMaterial.BuildingMaterial =
                    _materialDb.BuildingMaterials.Get(workBuildingMaterial.MaterialId);
                
                work.BuildingMaterials.Add(workBuildingMaterial);
            }
            else
            {
                dbBuildingMaterial.Count += t.Count;
            }

            _updatePrices(work);
            
            _db.Save();

            return 1;
        }

        public void Update(int workId, WorkBuildingMaterialViewModel t)
        {
            var work = _db.Works.All()
                .Include(w => w.BuildingMaterials)
                .ThenInclude(bm => bm.BuildingMaterial)
                .FirstOrDefault(w => w.Id == workId);

            if (work == null)
                throw new ServiceException(UiConstants.ObjectNotFound);

            var dbBuildingMaterial = work.BuildingMaterials.FirstOrDefault(bm => bm.MaterialId == t.MaterialId);
            if (dbBuildingMaterial == null)
                throw new ServiceException(UiConstants.ObjectNotFound);

            _mapper.Map(t, dbBuildingMaterial);

            _updatePrices(work);
            
            _db.Save();
        }

        public void Delete(int workId, int id)
        {
            var work = _db.Works.All()
                .Include(w => w.BuildingMaterials)
                .ThenInclude(bm => bm.BuildingMaterial)
                .FirstOrDefault(w => w.Id == workId);

            if (work == null)
                throw new ServiceException(UiConstants.ObjectNotFound);

            var dbBuildingMaterial = work.BuildingMaterials.FirstOrDefault(bm => bm.MaterialId == id);
            if (dbBuildingMaterial == null)
                throw new ServiceException(UiConstants.ObjectNotFound);

            work.BuildingMaterials.Remove(dbBuildingMaterial);

            _updatePrices(work);
            
            _db.Save();
        }

        private void _updatePrices(Work work)
        {
            var category = _db.WorkCategories.Get(work.WorkCategoryId);
            var phase = _db.Phases.Get(category.PhaseId);
            var building = _db.Buildings.Get(phase.BuildingId);
            
            building.FullPrice -= phase.FullPrice;
            phase.FullPrice -= category.FullPrice;
            category.FullPrice -= work.FullPrice;
            
            work.PriceForBMaterials = work.BuildingMaterials.Sum(bm => bm.Count * bm.BuildingMaterial.Price * bm.BuildingMaterial.Coefficient);
            
            category.FullPrice += work.FullPrice;
            phase.FullPrice += category.FullPrice;
            building.FullPrice += phase.FullPrice;
        }
    }
}