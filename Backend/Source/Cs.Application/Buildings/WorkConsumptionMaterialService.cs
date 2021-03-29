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
    public class WorkConsumptionMaterialService : IDependedCrudOperation<WorkConsumptionMaterialViewModel>
    {
        private readonly IBuildingDbService _db;
        private readonly IMaterialDbService _materialDb;
        private readonly IMapper _mapper;

        public WorkConsumptionMaterialService(IBuildingDbService db, IMaterialDbService materialDb, IMapper mapper)
        {
            _db = db;
            _materialDb = materialDb;
            _mapper = mapper;
        }

        public ListResult<WorkConsumptionMaterialViewModel> Get(int workId, Paging paging)
        {
            var work = _db.Works.All()
                .Include(w => w.ConsumptionMaterials)
                .ThenInclude(bm => bm.Material)
                .ThenInclude(bm => bm.Category)
                .Include(w => w.ConsumptionMaterials)
                .ThenInclude(bm => bm.Material)
                .ThenInclude(bm => bm.Dimension)
                .FirstOrDefault(w => w.Id == workId);
            if (work == null)
                throw new ServiceException(UiConstants.ObjectNotFound);

            var consumptionMaterials = work.ConsumptionMaterials;
            if (!string.IsNullOrEmpty(paging.Search))
                consumptionMaterials = consumptionMaterials.Where(t => t.Material.Name.Contains(paging.Search))
                    .ToList();

            var result = consumptionMaterials.Skip(paging.Skip).Take(paging.Count).ToList();
            var total = consumptionMaterials.Count();

            var viewModels = _mapper.Map<List<WorkConsumptionMaterialViewModel>>(result);

            return new ListResult<WorkConsumptionMaterialViewModel>(viewModels, total);
        }

        public WorkConsumptionMaterialViewModel Get(int workId, int id)
        {
            var work = _db.Works.All()
                .Include(w => w.ConsumptionMaterials)
                .ThenInclude(bm => bm.Material)
                .ThenInclude(bm => bm.Category)
                .Include(w => w.ConsumptionMaterials)
                .ThenInclude(bm => bm.Material)
                .ThenInclude(bm => bm.Dimension)
                .FirstOrDefault(w => w.Id == workId);

            if (work == null)
                throw new ServiceException(UiConstants.ObjectNotFound);

            var consumptionMaterial = work.ConsumptionMaterials.FirstOrDefault(bm => bm.MaterialId == id);
            if (consumptionMaterial == null)
                throw new ServiceException(UiConstants.ObjectNotFound);

            return _mapper.Map<WorkConsumptionMaterialViewModel>(consumptionMaterial);
        }

        public int Add(int workId, WorkConsumptionMaterialViewModel t)
        {
            var work = _db.Works.All()
                .Include(w => w.ConsumptionMaterials)
                .ThenInclude(bm => bm.Material)
                .FirstOrDefault(w => w.Id == workId);

            if (work == null)
                throw new ServiceException(UiConstants.ObjectNotFound);

            var dbConsumptionMaterial = work.ConsumptionMaterials.FirstOrDefault(bm => bm.MaterialId == t.MaterialId);
            if (dbConsumptionMaterial == null)
            {
                var workConsumptionMaterial = _mapper.Map<WorkConsumptionMaterial>(t);
                workConsumptionMaterial.Material =
                    _materialDb.ConsumptionMaterials.Get(workConsumptionMaterial.MaterialId);

                work.ConsumptionMaterials.Add(workConsumptionMaterial);
            }
            else
            {
                dbConsumptionMaterial.Count += t.Count;
            }

            _updatePrices(work);

            _db.Save();

            return 1;
        }

        public void Update(int workId, WorkConsumptionMaterialViewModel t)
        {
            var work = _db.Works.All()
                .Include(w => w.ConsumptionMaterials)
                .ThenInclude(bm => bm.Material)
                .FirstOrDefault(w => w.Id == workId);

            if (work == null)
                throw new ServiceException(UiConstants.ObjectNotFound);

            var dbConsumptionMaterial = work.ConsumptionMaterials.FirstOrDefault(bm => bm.MaterialId == t.MaterialId);
            if (dbConsumptionMaterial == null)
                throw new ServiceException(UiConstants.ObjectNotFound);

            _mapper.Map(t, dbConsumptionMaterial);

            _updatePrices(work);

            _db.Save();
        }

        public void Delete(int workId, int id)
        {
            var work = _db.Works.All()
                .Include(w => w.ConsumptionMaterials)
                .ThenInclude(bm => bm.Material)
                .FirstOrDefault(w => w.Id == workId);

            if (work == null)
                throw new ServiceException(UiConstants.ObjectNotFound);

            var dbConsumptionMaterial = work.ConsumptionMaterials.FirstOrDefault(bm => bm.MaterialId == id);
            if (dbConsumptionMaterial == null)
                throw new ServiceException(UiConstants.ObjectNotFound);

            work.ConsumptionMaterials.Remove(dbConsumptionMaterial);

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

            work.PriceForCMaterials =
                work.ConsumptionMaterials.Sum(bm => bm.Count * bm.Material.Price * bm.Material.Coefficient);

            category.FullPrice += work.FullPrice;
            phase.FullPrice += category.FullPrice;
            building.FullPrice += phase.FullPrice;
        }
    }
}