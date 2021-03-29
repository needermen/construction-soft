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
    public class WorkWorkerService : IDependedCrudOperation<WorkWorkerViewModel>
    {
        private readonly IBuildingDbService _db;
        private readonly IHrDbService _hrDb;
        private readonly IMapper _mapper;

        public WorkWorkerService(IBuildingDbService db, IHrDbService hrDb, IMapper mapper)
        {
            _db = db;
            _hrDb = hrDb;
            _mapper = mapper;
        }

        public ListResult<WorkWorkerViewModel> Get(int workId, Paging paging)
        {
            var work = _db.Works.All()
                .Include(w => w.Workers)
                .ThenInclude(bm => bm.Worker)
                .ThenInclude(bm => bm.Category)
                .Include(w => w.Workers)
                .ThenInclude(bm => bm.Worker)
                .ThenInclude(bm => bm.PaymentType)
                .FirstOrDefault(w => w.Id == workId);
            if (work == null)
                throw new ServiceException(UiConstants.ObjectNotFound);

            var mainWorkers = work.Workers;
            if (!string.IsNullOrEmpty(paging.Search))
                mainWorkers = mainWorkers.Where(t => t.Worker.Name.Contains(paging.Search))
                    .ToList();

            var result = mainWorkers.Skip(paging.Skip).Take(paging.Count).ToList();
            var total = mainWorkers.Count();

            var viewModels = _mapper.Map<List<WorkWorkerViewModel>>(result);

            return new ListResult<WorkWorkerViewModel>(viewModels, total);
        }

        public WorkWorkerViewModel Get(int workId, int id)
        {
            var work = _db.Works.All()
                .Include(w => w.Workers)
                .ThenInclude(bm => bm.Worker)
                .ThenInclude(bm => bm.Category)
                .Include(w => w.Workers)
                .ThenInclude(bm => bm.Worker)
                .ThenInclude(bm => bm.PaymentType)
                .FirstOrDefault(w => w.Id == workId);

            if (work == null)
                throw new ServiceException(UiConstants.ObjectNotFound);

            var mainWorker = work.Workers.FirstOrDefault(bm => bm.WorkerId == id);
            if (mainWorker == null)
                throw new ServiceException(UiConstants.ObjectNotFound);

            return _mapper.Map<WorkWorkerViewModel>(mainWorker);
        }

        public int Add(int workId, WorkWorkerViewModel t)
        {
            var work = _db.Works.All()
                .Include(w => w.Workers)
                .ThenInclude(bm => bm.Worker)
                .FirstOrDefault(w => w.Id == workId);

            if (work == null)
                throw new ServiceException(UiConstants.ObjectNotFound);

            var dbWorker = work.Workers.FirstOrDefault(bm => bm.WorkerId == t.WorkerId);
            if (dbWorker == null)
            {
                var workWorker = _mapper.Map<WorkWorker>(t);
                workWorker.Worker = _hrDb.Workers.Get(workWorker.WorkerId);

                work.Workers.Add(workWorker);
            }
            else
            {
                dbWorker.Count += t.Count;
            }

            _updatePrices(work);

            _db.Save();

            return 1;
        }

        public void Update(int workId, WorkWorkerViewModel t)
        {
            var work = _db.Works.All()
                .Include(w => w.Workers)
                .ThenInclude(bm => bm.Worker)
                .FirstOrDefault(w => w.Id == workId);

            if (work == null)
                throw new ServiceException(UiConstants.ObjectNotFound);

            var dbWorker = work.Workers.FirstOrDefault(bm => bm.WorkerId == t.WorkerId);
            if (dbWorker == null)
                throw new ServiceException(UiConstants.ObjectNotFound);

            _mapper.Map(t, dbWorker);

            _updatePrices(work);

            _db.Save();
        }

        public void Delete(int workId, int id)
        {
            var work = _db.Works.All()
                .Include(w => w.Workers)
                .ThenInclude(bm => bm.Worker)
                .FirstOrDefault(w => w.Id == workId);

            if (work == null)
                throw new ServiceException(UiConstants.ObjectNotFound);

            var dbWorker = work.Workers.FirstOrDefault(bm => bm.WorkerId == id);
            if (dbWorker == null)
                throw new ServiceException(UiConstants.ObjectNotFound);

            work.Workers.Remove(dbWorker);

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

            work.PriceForWorkers = work.Workers.Sum(bm => bm.Count * bm.Worker.Salary * bm.Worker.Coefficient);

            category.FullPrice += work.FullPrice;
            phase.FullPrice += category.FullPrice;
            building.FullPrice += phase.FullPrice;
        }
    }
}