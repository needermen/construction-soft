using System.Linq;
using Cs.Application.Interfaces;
using Cs.Application.Tools;
using Cs.Common.Models;
using Cs.Domain.Materials;

namespace Cs.Application.Material
{
    public class ConsumptionMaterialCategoryService : ICrudOperation<ConsumptionMaterialCategory>
    {
        private readonly IMaterialDbService _db;

        public ConsumptionMaterialCategoryService(IMaterialDbService db)
        {
            _db = db;
        }

        public ListResult<ConsumptionMaterialCategory> Get(Paging paging)
        {
            var materialCategories = _db.ConsumptionMaterialCategories.All();
            if (!string.IsNullOrEmpty(paging.Search))
                materialCategories = materialCategories.Where(t => t.Name.Contains(paging.Search));
            
            var result = materialCategories.Skip(paging.Skip).Take(paging.Count).ToList();
            var total = materialCategories.Count();
            
            return new ListResult<ConsumptionMaterialCategory>(result, total);
        }

        public ConsumptionMaterialCategory Get(int id)
        {
            return _db.ConsumptionMaterialCategories.Get(id);
        }

        public int Add(ConsumptionMaterialCategory t)
        {
            if (_db.ConsumptionMaterialCategories.All().Any(bc => bc.Name == t.Name))
                throw new ServiceException(UiConstants.EntityWithSameNameAlreadyExists(t.Name));

            _db.ConsumptionMaterialCategories.Add(t);
            _db.ConsumptionMaterialCategories.Save();

            return t.Id;
        }

        public void Update(ConsumptionMaterialCategory t)
        {
            if (_db.ConsumptionMaterialCategories.All().Any(bc => bc.Name == t.Name && bc.Id != t.Id))
                throw new ServiceException(UiConstants.EntityWithSameNameAlreadyExists(t.Name));

            _db.ConsumptionMaterialCategories.Update(t);
            _db.ConsumptionMaterialCategories.Save();
        }

        public void Delete(int id)
        {
            _db.ConsumptionMaterialCategories.Delete(id);
            _db.ConsumptionMaterialCategories.Save();
        }
    }
}