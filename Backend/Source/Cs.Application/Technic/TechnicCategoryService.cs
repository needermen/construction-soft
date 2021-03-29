using System.Linq;
using Cs.Application.Interfaces;
using Cs.Application.Tools;
using Cs.Common.Models;
using Cs.Domain.Technics;

namespace Cs.Application.Technic
{
    public class TechnicCategoryService : ICrudOperation<TechnicCategory>
    {
        private readonly ITechnicDbService _db;

        public TechnicCategoryService(ITechnicDbService db)
        {
            _db = db;
        }
        
        public ListResult<TechnicCategory> Get(Paging paging)
        {
            var tcategories = _db.TechnicCategories.All();
            if (!string.IsNullOrEmpty(paging.Search))
                tcategories = tcategories.Where(t => t.Name.Contains(paging.Search));
            
            var result = tcategories.Skip(paging.Skip).Take(paging.Count).ToList();
            var total = tcategories.Count();
            
            return new ListResult<TechnicCategory>(result, total);
        }

        public TechnicCategory Get(int id)
        {
            return _db.TechnicCategories.Get(id);
        }

        public int Add(TechnicCategory t)
        {
            if (_db.TechnicCategories.All().Any(bc => bc.Name == t.Name))
                throw new ServiceException(UiConstants.EntityWithSameNameAlreadyExists(t.Name));

            _db.TechnicCategories.Add(t);
            _db.TechnicCategories.Save();

            return t.Id;
        }

        public void Update(TechnicCategory t)
        {
            if (_db.TechnicCategories.All().Any(bc => bc.Name == t.Name && bc.Id != t.Id))
                throw new ServiceException(UiConstants.EntityWithSameNameAlreadyExists(t.Name));

            _db.TechnicCategories.Update(t);
            _db.TechnicCategories.Save();
        }

        public void Delete(int id)
        {
            _db.TechnicCategories.Delete(id);
            _db.TechnicCategories.Save();
        }
    }
}