using System.Linq;
using Cs.Application.Interfaces;
using Cs.Application.Tools;
using Cs.Common.Models;
using Cs.Domain.Materials;
using Microsoft.EntityFrameworkCore;

namespace Cs.Application.Material
{
    public class MainMaterialService : ICrudOperation<MainMaterial>
    {
        private readonly IMaterialDbService _db;

        public MainMaterialService(IMaterialDbService db)
        {
            _db = db;
        }

        public ListResult<MainMaterial> Get(Paging paging)
        {
            var materials = _db.MainMaterials.All();
            if (!string.IsNullOrEmpty(paging.Search))
                materials = materials.Where(t => t.Name.Contains(paging.Search)
                                                 || t.Comment.Contains(paging.Search)
                                                 || t.Dimension.Name.Contains(paging.Search)
                                                 || t.Category.Name.Contains(paging.Search));

            var result = materials.Include(t => t.Category).Include(t => t.Dimension).Skip(paging.Skip)
                .Take(paging.Count).ToList();
            var total = materials.Count();

            return new ListResult<MainMaterial>(result, total);
        }

        public MainMaterial Get(int id)
        {
            return _db.MainMaterials.Get(id);
        }

        public int Add(MainMaterial t)
        {
            if (_db.MainMaterials.All().Any(bc => bc.Name == t.Name))
                throw new ServiceException(UiConstants.EntityWithSameNameAlreadyExists(t.Name));

            _db.MainMaterials.Add(t);
            _db.MainMaterials.Save();

            return t.Id;
        }

        public void Update(MainMaterial t)
        {
            if (_db.MainMaterials.All().Any(bc => bc.Name == t.Name && bc.Id != t.Id))
                throw new ServiceException(UiConstants.EntityWithSameNameAlreadyExists(t.Name));

            _db.MainMaterials.Update(t);
            _db.MainMaterials.Save();
        }

        public void Delete(int id)
        {
            _db.MainMaterials.Delete(id);
            _db.MainMaterials.Save();
        }
    }
}