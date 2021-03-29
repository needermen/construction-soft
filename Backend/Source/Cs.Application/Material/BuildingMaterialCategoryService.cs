using System.Linq;
using Cs.Application.Interfaces;
using Cs.Application.Tools;
using Cs.Common.Models;
using Cs.Domain.Materials;

namespace Cs.Application.Material
{
    public class BuildingMaterialCategoryService : ICrudOperation<BuildingMaterialCategory>
    {
        private readonly IMaterialDbService _db;

        public BuildingMaterialCategoryService(IMaterialDbService db)
        {
            _db = db;
        }

        public ListResult<BuildingMaterialCategory> Get(Paging paging)
        {
            var materialCategories = _db.BuildingMaterialCategories.All();
            if (!string.IsNullOrEmpty(paging.Search))
                materialCategories = materialCategories.Where(t => t.Name.Contains(paging.Search));
            
            var result = materialCategories.Skip(paging.Skip).Take(paging.Count).ToList();
            var total = materialCategories.Count();
            
            return new ListResult<BuildingMaterialCategory>(result, total);
        }

        public BuildingMaterialCategory Get(int id)
        {
            return _db.BuildingMaterialCategories.Get(id);
        }

        public int Add(BuildingMaterialCategory t)
        {
            if (_db.BuildingMaterialCategories.All().Any(bc => bc.Name == t.Name))
                throw new ServiceException(UiConstants.EntityWithSameNameAlreadyExists(t.Name));

            _db.BuildingMaterialCategories.Add(t);
            _db.BuildingMaterialCategories.Save();

            return t.Id;
        }

        public void Update(BuildingMaterialCategory t)
        {
            if (_db.BuildingMaterialCategories.All().Any(bc => bc.Name == t.Name && bc.Id != t.Id))
                throw new ServiceException(UiConstants.EntityWithSameNameAlreadyExists(t.Name));

            _db.BuildingMaterialCategories.Update(t);
            _db.BuildingMaterialCategories.Save();
        }

        public void Delete(int id)
        {
            _db.BuildingMaterialCategories.Delete(id);
            _db.BuildingMaterialCategories.Save();
        }
    }
}