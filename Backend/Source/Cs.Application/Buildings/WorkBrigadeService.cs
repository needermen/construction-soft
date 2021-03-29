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
    public class WorkBrigadeService : IDependedCrudOperation<WorkBrigadeViewModel>
    {
        private readonly IBuildingDbService _db;
        private readonly IHrDbService _hrDb;
        private readonly IMapper _mapper;

        public WorkBrigadeService (IBuildingDbService db, IHrDbService hrDb, IMapper mapper)
        {
            _db = db;
            _hrDb = hrDb;
            _mapper = mapper;
        }

        public ListResult<WorkBrigadeViewModel> Get(int workId, Paging paging)
        {
            var work = _db.Works.All()
                .Include(w => w.Brigades)
                .ThenInclude(bm => bm.Brigade)
                .ThenInclude(bm => bm.Category)
                .Include(w => w.Brigades)
                .ThenInclude(bm => bm.Brigade)
                .ThenInclude(bm => bm.PaymentType)
                .FirstOrDefault(w => w.Id == workId);
            if (work == null)
                throw new ServiceException(UiConstants.ObjectNotFound);

            var mainBrigades = work.Brigades;
            if (!string.IsNullOrEmpty(paging.Search))
                mainBrigades = mainBrigades.Where(t => t.Brigade.Name.Contains(paging.Search))
                    .ToList();

            var result = mainBrigades.Skip(paging.Skip).Take(paging.Count).ToList();
            var total = mainBrigades.Count();

            var viewModels = _mapper.Map<List<WorkBrigadeViewModel>>(result);

            return new ListResult<WorkBrigadeViewModel>(viewModels, total);
        }

        public WorkBrigadeViewModel Get(int workId, int id)
        {
            var work = _db.Works.All()
                .Include(w => w.Brigades)
                .ThenInclude(bm => bm.Brigade)
                .ThenInclude(bm => bm.Category)
                .Include(w => w.Brigades)
                .ThenInclude(bm => bm.Brigade)
                .ThenInclude(bm => bm.PaymentType)
                .FirstOrDefault(w => w.Id == workId);

            if (work == null)
                throw new ServiceException(UiConstants.ObjectNotFound);

            var mainBrigade = work.Brigades.FirstOrDefault(bm => bm.BrigadeId == id);
            if (mainBrigade == null)
                throw new ServiceException(UiConstants.ObjectNotFound);

            return _mapper.Map<WorkBrigadeViewModel>(mainBrigade);
        }

        public int Add(int workId, WorkBrigadeViewModel t)
        {
            var work = _db.Works.All()
                .Include(w => w.Brigades)
                .ThenInclude(bm => bm.Brigade)
                .FirstOrDefault(w => w.Id == workId);

            if (work == null)
                throw new ServiceException(UiConstants.ObjectNotFound);

            var dbBrigade = work.Brigades.FirstOrDefault(bm => bm.BrigadeId == t.BrigadeId);
            if (dbBrigade == null)
            {
                var workBrigade = _mapper.Map<WorkBrigade>(t);
                workBrigade.Brigade = _hrDb.Brigades.Get(workBrigade.BrigadeId);
                
                work.Brigades.Add(workBrigade);
            }
            else
            {
                dbBrigade.Count += t.Count;
            }

            _updatePrices(work);
            
            _db.Save();

            return 1;
        }

        public void Update(int workId, WorkBrigadeViewModel t)
        {
            var work = _db.Works.All()
                .Include(w => w.Brigades)
                .ThenInclude(bm => bm.Brigade)
                .FirstOrDefault(w => w.Id == workId);

            if (work == null)
                throw new ServiceException(UiConstants.ObjectNotFound);

            var dbBrigade = work.Brigades.FirstOrDefault(bm => bm.BrigadeId == t.BrigadeId);
            if (dbBrigade == null)
                throw new ServiceException(UiConstants.ObjectNotFound);

            _mapper.Map(t, dbBrigade);

            _updatePrices(work);
            
            _db.Save();
        }

        public void Delete(int workId, int id)
        {
            var work = _db.Works.All()
                .Include(w => w.Brigades)
                .ThenInclude(bm => bm.Brigade)
                .FirstOrDefault(w => w.Id == workId);

            if (work == null)
                throw new ServiceException(UiConstants.ObjectNotFound);

            var dbBrigade = work.Brigades.FirstOrDefault(bm => bm.BrigadeId == id);
            if (dbBrigade == null)
                throw new ServiceException(UiConstants.ObjectNotFound);

            work.Brigades.Remove(dbBrigade);

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
            
            work.PriceForBrigades = work.Brigades.Sum(bm => bm.Count * bm.Brigade.Salary);
            
            category.FullPrice += work.FullPrice;
            phase.FullPrice += category.FullPrice;
            building.FullPrice += phase.FullPrice;
        }
    }
}