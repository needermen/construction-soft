using System.Linq;
using Cs.Application.Interfaces;
using Cs.Application.Tools;
using Cs.Common.Models;
using Cs.Domain.Materials;

namespace Cs.Application.Material
{
    public class MaterialDimensionService : ICrudOperation<MaterialDimension>
    {
        private readonly IMaterialDbService _db;

        public MaterialDimensionService(IMaterialDbService db)
        {
            _db = db;
        }

        public ListResult<MaterialDimension> Get(Paging paging)
        {
            var dimensions = _db.MaterialDimensions.All();
            if (!string.IsNullOrEmpty(paging.Search))
                dimensions = dimensions.Where(t => t.Name.Contains(paging.Search));
            
            var result = dimensions.Skip(paging.Skip).Take(paging.Count).ToList();
            var total = dimensions.Count();
            
            return new ListResult<MaterialDimension>(result, total);
        }

        public MaterialDimension Get(int id)
        {
            return _db.MaterialDimensions.Get(id);
        }

        public int Add(MaterialDimension t)
        {
            if (_db.MaterialDimensions.All().Any(bc => bc.Name == t.Name))
                throw new ServiceException(UiConstants.EntityWithSameNameAlreadyExists(t.Name));

            _db.MaterialDimensions.Add(t);
            _db.MaterialDimensions.Save();

            return t.Id;
        }

        public void Update(MaterialDimension t)
        {
            if (_db.MaterialDimensions.All().Any(bc => bc.Name == t.Name && bc.Id != t.Id))
                throw new ServiceException(UiConstants.EntityWithSameNameAlreadyExists(t.Name));

            _db.MaterialDimensions.Update(t);
            _db.MaterialDimensions.Save();
        }

        public void Delete(int id)
        {
            _db.MaterialDimensions.Delete(id);
            _db.MaterialDimensions.Save();
        }
    }
}