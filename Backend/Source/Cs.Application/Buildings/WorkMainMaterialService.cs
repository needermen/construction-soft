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
    public class WorkMainMaterialService : IDependedCrudOperation<WorkMainMaterialViewModel>
    {
        private readonly IBuildingDbService _db;
        private readonly IMaterialDbService _materialDb;
        private readonly IMapper _mapper;

        public WorkMainMaterialService (IBuildingDbService db, IMaterialDbService materialDb, IMapper mapper)
        {
            _db = db;
            _materialDb = materialDb;
            _mapper = mapper;
        }

        public ListResult<WorkMainMaterialViewModel> Get(int workId, Paging paging)
        {
            var work = _db.Works.All()
                .Include(w => w.MainMaterials)
                .ThenInclude(bm => bm.Material)
                .ThenInclude(bm => bm.Category)
                .Include(w => w.MainMaterials)
                .ThenInclude(bm => bm.Material)
                .ThenInclude(bm => bm.Dimension)
                .FirstOrDefault(w => w.Id == workId);
            if (work == null)
                throw new ServiceException(UiConstants.ObjectNotFound);

            var mainMaterials = work.MainMaterials;
            if (!string.IsNullOrEmpty(paging.Search))
                mainMaterials = mainMaterials.Where(t => t.Material.Name.Contains(paging.Search))
                    .ToList();

            var result = mainMaterials.Skip(paging.Skip).Take(paging.Count).ToList();
            var total = mainMaterials.Count();

            var viewModels = _mapper.Map<List<WorkMainMaterialViewModel>>(result);

            return new ListResult<WorkMainMaterialViewModel>(viewModels, total);
        }

        public WorkMainMaterialViewModel Get(int workId, int id)
        {
            var work = _db.Works.All()
                .Include(w => w.MainMaterials)
                .ThenInclude(bm => bm.Material)
                .ThenInclude(bm => bm.Category)
                .Include(w => w.MainMaterials)
                .ThenInclude(bm => bm.Material)
                .ThenInclude(bm => bm.Dimension)
                .FirstOrDefault(w => w.Id == workId);

            if (work == null)
                throw new ServiceException(UiConstants.ObjectNotFound);

            var mainMaterial = work.MainMaterials.FirstOrDefault(bm => bm.MaterialId == id);
            if (mainMaterial == null)
                throw new ServiceException(UiConstants.ObjectNotFound);

            return _mapper.Map<WorkMainMaterialViewModel>(mainMaterial);
        }

        public int Add(int workId, WorkMainMaterialViewModel t)
        {
            var work = _db.Works.All()
                .Include(w => w.MainMaterials)
                .ThenInclude(bm => bm.Material)
                .FirstOrDefault(w => w.Id == workId);

            if (work == null)
                throw new ServiceException(UiConstants.ObjectNotFound);

            var dbMainMaterial = work.MainMaterials.FirstOrDefault(bm => bm.MaterialId == t.MaterialId);
            if (dbMainMaterial == null)
            {
                var workMainMaterial = _mapper.Map<WorkMainMaterial>(t);
                workMainMaterial.Material = _materialDb.MainMaterials.Get(workMainMaterial.MaterialId);
                
                work.MainMaterials.Add(workMainMaterial);
            }
            else
            {
                dbMainMaterial.Count += t.Count;
            }

            _updatePrices(work);
            
            _db.Save();

            return 1;
        }

        public void Update(int workId, WorkMainMaterialViewModel t)
        {
            var work = _db.Works.All()
                .Include(w => w.MainMaterials)
                .ThenInclude(bm => bm.Material)
                .FirstOrDefault(w => w.Id == workId);

            if (work == null)
                throw new ServiceException(UiConstants.ObjectNotFound);

            var dbMainMaterial = work.MainMaterials.FirstOrDefault(bm => bm.MaterialId == t.MaterialId);
            if (dbMainMaterial == null)
                throw new ServiceException(UiConstants.ObjectNotFound);

            _mapper.Map(t, dbMainMaterial);

            _updatePrices(work);
            
            _db.Save();
        }

        public void Delete(int workId, int id)
        {
            var work = _db.Works.All()
                .Include(w => w.MainMaterials)
                .ThenInclude(bm => bm.Material)
                .FirstOrDefault(w => w.Id == workId);

            if (work == null)
                throw new ServiceException(UiConstants.ObjectNotFound);

            var dbMainMaterial = work.MainMaterials.FirstOrDefault(bm => bm.MaterialId == id);
            if (dbMainMaterial == null)
                throw new ServiceException(UiConstants.ObjectNotFound);

            work.MainMaterials.Remove(dbMainMaterial);

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
            
            work.PriceForMMaterials = work.MainMaterials.Sum(bm => bm.Count * bm.Material.Price * bm.Material.Coefficient);
            
            category.FullPrice += work.FullPrice;
            phase.FullPrice += category.FullPrice;
            building.FullPrice += phase.FullPrice;
        }
    }
}