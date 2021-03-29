using System.Linq;
using Cs.Application.Interfaces;
using Cs.Application.Tools;
using Cs.Common.Models;
using Cs.Domain.Hr;

namespace Cs.Application.Hr
{
    public class WorkerCategoryService : ICrudOperation<WorkerCategory>
    {
        private readonly IHrDbService _db;

        public WorkerCategoryService(IHrDbService db)
        {
            _db = db;
        }

        public ListResult<WorkerCategory> Get(Paging paging)
        {
            var workerCategories = _db.WorkerCategories.All();
            if (!string.IsNullOrEmpty(paging.Search))
                workerCategories = workerCategories.Where(t => t.Name.Contains(paging.Search));
            
            var result = workerCategories.Skip(paging.Skip).Take(paging.Count).ToList();
            var total = workerCategories.Count();
            
            return new ListResult<WorkerCategory>(result, total);
        }

        public WorkerCategory Get(int id)
        {
            return _db.WorkerCategories.Get(id);
        }

        public int Add(WorkerCategory t)
        {
            if (_db.WorkerCategories.All().Any(bc => bc.Name == t.Name))
                throw new ServiceException(UiConstants.EntityWithSameNameAlreadyExists(t.Name));

            _db.WorkerCategories.Add(t);
            _db.WorkerCategories.Save();

            return t.Id;
        }

        public void Update(WorkerCategory t)
        {
            if (_db.WorkerCategories.All().Any(bc => bc.Name == t.Name && bc.Id != t.Id))
                throw new ServiceException(UiConstants.EntityWithSameNameAlreadyExists(t.Name));

            _db.WorkerCategories.Update(t);
            _db.WorkerCategories.Save();
        }

        public void Delete(int id)
        {
            _db.WorkerCategories.Delete(id);
            _db.WorkerCategories.Save();
        }
    }
}