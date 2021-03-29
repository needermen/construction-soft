using System.Linq;
using Cs.Application.Interfaces;
using Cs.Application.Tools;
using Cs.Common.Models;
using Cs.Domain.Materials;
using Microsoft.EntityFrameworkCore;

namespace Cs.Application.Material
{
    public class BuildingMaterialService : ICrudOperation<BuildingMaterial>
    {
        private readonly IMaterialDbService _db;

        public BuildingMaterialService(IMaterialDbService db)
        {
            _db = db;
        }

        public ListResult<BuildingMaterial> Get(Paging paging)
        {
            var materials = _db.BuildingMaterials.All();
            if (!string.IsNullOrEmpty(paging.Search))
                materials = materials.Where(t => t.Name.Contains(paging.Search)
                                                 || t.Comment.Contains(paging.Search)
                                                 || t.Dimension.Name.Contains(paging.Search)
                                                 || t.Category.Name.Contains(paging.Search));

            var result = materials.Include(t => t.Category).Include(t => t.Dimension).Skip(paging.Skip)
                .Take(paging.Count).ToList();
            var total = materials.Count();

            return new ListResult<BuildingMaterial>(result, total);
        }

        public BuildingMaterial Get(int id)
        {
            return _db.BuildingMaterials.Get(id);
        }

        public int Add(BuildingMaterial t)
        {
            if (_db.BuildingMaterials.All().Any(bc => bc.Name == t.Name))
                throw new ServiceException(UiConstants.EntityWithSameNameAlreadyExists(t.Name));

            _db.BuildingMaterials.Add(t);
            _db.BuildingMaterials.Save();

            return t.Id;
        }

        public void Update(BuildingMaterial t)
        {
            if (_db.BuildingMaterials.All().Any(bc => bc.Name == t.Name && bc.Id != t.Id))
                throw new ServiceException(UiConstants.EntityWithSameNameAlreadyExists(t.Name));

            _db.BuildingMaterials.Update(t);
            _db.BuildingMaterials.Save();
        }

        public void Delete(int id)
        {
            _db.BuildingMaterials.Delete(id);
            _db.BuildingMaterials.Save();
        }
    }
}