using System;
using System.Linq;
using Cs.Application.Interfaces;
using Cs.Application.Tools;
using Cs.Common.Models;
using Cs.Domain.Hr;

namespace Cs.Application.Hr
{
    public class BrigadeCategoryService : ICrudOperation<BrigadeCategory>
    {
        private readonly IHrDbService _db;

        public BrigadeCategoryService(IHrDbService db)
        {
            _db = db;
        }

        public ListResult<BrigadeCategory> Get(Paging paging)
        {
            var brigadeCategories = _db.BrigadeCategories.All();
            if (!string.IsNullOrEmpty(paging.Search))
                brigadeCategories = brigadeCategories.Where(t => t.Name.Contains(paging.Search));

            var result = brigadeCategories.Skip(paging.Skip).Take(paging.Count).ToList();
            var total = brigadeCategories.Count();

            return new ListResult<BrigadeCategory>(result, total);
        }

        public BrigadeCategory Get(int id)
        {
            return _db.BrigadeCategories.Get(id);
        }

        public int Add(BrigadeCategory t)
        {
            if (_db.BrigadeCategories.All().Any(bc => bc.Name == t.Name))
                throw new ServiceException(UiConstants.EntityWithSameNameAlreadyExists(t.Name));

            _db.BrigadeCategories.Add(t);
            _db.BrigadeCategories.Save();

            return t.Id;
        }

        public void Update(BrigadeCategory t)
        {
            if (_db.BrigadeCategories.All().Any(bc => bc.Name == t.Name && bc.Id != t.Id))
                throw new ServiceException(UiConstants.EntityWithSameNameAlreadyExists(t.Name));

            _db.BrigadeCategories.Update(t);
            _db.BrigadeCategories.Save();
        }

        public void Delete(int id)
        {
            _db.BrigadeCategories.Delete(id);
            _db.BrigadeCategories.Save();
        }
    }
}