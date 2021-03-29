using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Cs.Application.Hr.Models;
using Cs.Application.Interfaces;
using Cs.Application.Tools;
using Cs.Common.Models;
using Cs.Domain.Hr;
using Microsoft.EntityFrameworkCore;

namespace Cs.Application.Hr
{
    public class WorkerService : ICrudOperation<WorkerViewModel>
    {
        private readonly IHrDbService _db;
        private readonly IMapper _mapper;

        public WorkerService(IHrDbService db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public ListResult<WorkerViewModel> Get(Paging paging)
        {
            var workers = _db.Workers.All();
            if (!string.IsNullOrEmpty(paging.Search))
                workers = workers.Where(t => t.Name.Contains(paging.Search)
                                             || t.Comment.Contains(paging.Search)
                                             || t.PaymentType.Name.Contains(paging.Search)
                                             || t.Category.Name.Contains(paging.Search));

            var result = workers.Include(t => t.Category).Include(t => t.PaymentType).Skip(paging.Skip)
                .Take(paging.Count).ToList();

            var total = workers.Count();

            var viewModels = _mapper.Map<List<WorkerViewModel>>(result);

            return new ListResult<WorkerViewModel>(viewModels, total);
        }

        public WorkerViewModel Get(int id)
        {
            var worker = _db.Workers.All()
                .Include(b => b.Files)
                .ThenInclude(f => f.File)
                .FirstOrDefault(b => b.Id == id);

            return _mapper.Map<WorkerViewModel>(worker);
        }

        public int Add(WorkerViewModel t)
        {
            if (_db.Workers.All().Any(bc => bc.Name == t.Name))
                throw new ServiceException(UiConstants.EntityWithSameNameAlreadyExists(t.Name));

            var dbWorker = _mapper.Map<Worker>(t);

            _db.Workers.Add(dbWorker);
            _db.Workers.Save();

            return dbWorker.Id;
        }

        public void Update(WorkerViewModel t)
        {
            if (_db.Workers.All().Any(bc => bc.Name == t.Name && bc.Id != t.Id))
                throw new ServiceException(UiConstants.EntityWithSameNameAlreadyExists(t.Name));

            var worker = _db.Workers.All()
                .Include(b => b.Files)
                .FirstOrDefault(b => b.Id == t.Id);

            if (worker == null)
                throw new ServiceException(UiConstants.ObjectNotFound);

            _mapper.Map(t, worker);

            _db.Workers.Update(worker);
            _db.Workers.Save();
        }

        public void Delete(int id)
        {
            _db.Workers.Delete(id);
            _db.Workers.Save();
        }
    }
}