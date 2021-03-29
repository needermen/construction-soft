using System.Linq;
using Cs.Application.Interfaces;
using Cs.Application.Tools;
using Cs.Common.Models;
using Cs.Domain.Materials;

namespace Cs.Application.Material
{
    public class MainMaterialCategoryService : ICrudOperation<MainMaterialCategory>
    {
        private readonly IMaterialDbService _db;

        public MainMaterialCategoryService(IMaterialDbService db)
        {
            _db = db;
        }

        public ListResult<MainMaterialCategory> Get(Paging paging)
        {
            var materialCategories = _db.MainMaterialCategories.All();
            if (!string.IsNullOrEmpty(paging.Search))
                materialCategories = materialCategories.Where(t => t.Name.Contains(paging.Search));
            
            var result = materialCategories.Skip(paging.Skip).Take(paging.Count).ToList();
            var total = materialCategories.Count();
            
            return new ListResult<MainMaterialCategory>(result, total);
        }

        public MainMaterialCategory Get(int id)
        {
            return _db.MainMaterialCategories.Get(id);
        }

        public int Add(MainMaterialCategory t)
        {
            if (_db.MainMaterialCategories.All().Any(bc => bc.Name == t.Name))
                throw new ServiceException(UiConstants.EntityWithSameNameAlreadyExists(t.Name));

            _db.MainMaterialCategories.Add(t);
            _db.MainMaterialCategories.Save();

            return t.Id;
        }

        public void Update(MainMaterialCategory t)
        {
            if (_db.MainMaterialCategories.All().Any(bc => bc.Name == t.Name && bc.Id != t.Id))
                throw new ServiceException(UiConstants.EntityWithSameNameAlreadyExists(t.Name));

            _db.MainMaterialCategories.Update(t);
            _db.MainMaterialCategories.Save();
        }

        public void Delete(int id)
        {
            _db.MainMaterialCategories.Delete(id);
            _db.MainMaterialCategories.Save();
        }
    }
}