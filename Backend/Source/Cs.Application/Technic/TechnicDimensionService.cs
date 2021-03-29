using System.Linq;
using Cs.Application.Interfaces;
using Cs.Application.Tools;
using Cs.Common.Models;
using Cs.Domain.Technics;

namespace Cs.Application.Technic
{
    public class TechnicDimensionService : ICrudOperation<TechnicDimension>
    {
        private readonly ITechnicDbService _db;

        public TechnicDimensionService(ITechnicDbService db)
        {
            _db = db;
        }

        public ListResult<TechnicDimension> Get(Paging paging)
        {
            var tdimensions = _db.TechnicDimensions.All();
            if (!string.IsNullOrEmpty(paging.Search))
                tdimensions = tdimensions.Where(t => t.Name.Contains(paging.Search));
            
            var result = tdimensions.Skip(paging.Skip).Take(paging.Count).ToList();
            var total = tdimensions.Count();
            
            return new ListResult<TechnicDimension>(result, total);
        }

        public TechnicDimension Get(int id)
        {
            return _db.TechnicDimensions.Get(id);
        }

        public int Add(TechnicDimension t)
        {   
            if (_db.TechnicDimensions.All().Any(bc => bc.Name == t.Name))
                throw new ServiceException(UiConstants.EntityWithSameNameAlreadyExists(t.Name));

            _db.TechnicDimensions.Add(t);
            _db.TechnicDimensions.Save();

            return t.Id;
        }

        public void Update(TechnicDimension t)
        {
            if (_db.TechnicDimensions.All().Any(bc => bc.Name == t.Name && bc.Id != t.Id))
                throw new ServiceException(UiConstants.EntityWithSameNameAlreadyExists(t.Name));

            _db.TechnicDimensions.Update(t);
            _db.TechnicDimensions.Save();
        }

        public void Delete(int id)
        {
            _db.TechnicDimensions.Delete(id);
            _db.TechnicDimensions.Save();
        }
    }
}