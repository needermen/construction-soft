using System.Linq;
using Cs.Application.Interfaces;
using Cs.Application.Tools;
using Cs.Common.Models;
using Cs.Domain.Materials;
using Microsoft.EntityFrameworkCore;

namespace Cs.Application.Material
{
    public class ConsumptionMaterialService : ICrudOperation<ConsumptionMaterial>
    {
        private readonly IMaterialDbService _db;

        public ConsumptionMaterialService(IMaterialDbService db)
        {
            _db = db;
        }

        public ListResult<ConsumptionMaterial> Get(Paging paging)
        {
            var materials = _db.ConsumptionMaterials.All();
            if (!string.IsNullOrEmpty(paging.Search))
                materials = materials.Where(t => t.Name.Contains(paging.Search)
                                                 || t.Comment.Contains(paging.Search)
                                                 || t.Dimension.Name.Contains(paging.Search)
                                                 || t.Category.Name.Contains(paging.Search));

            var result = materials.Include(t => t.Category).Include(t => t.Dimension).Skip(paging.Skip)
                .Take(paging.Count).ToList();
            var total = materials.Count();

            return new ListResult<ConsumptionMaterial>(result, total);
        }

        public ConsumptionMaterial Get(int id)
        {
            return _db.ConsumptionMaterials.Get(id);
        }

        public int Add(ConsumptionMaterial t)
        {
            if (_db.ConsumptionMaterials.All().Any(bc => bc.Name == t.Name))
                throw new ServiceException(UiConstants.EntityWithSameNameAlreadyExists(t.Name));

            _db.ConsumptionMaterials.Add(t);
            _db.ConsumptionMaterials.Save();

            return t.Id;
        }

        public void Update(ConsumptionMaterial t)
        {
            if (_db.ConsumptionMaterials.All().Any(bc => bc.Name == t.Name && bc.Id != t.Id))
                throw new ServiceException(UiConstants.EntityWithSameNameAlreadyExists(t.Name));

            _db.ConsumptionMaterials.Update(t);
            _db.ConsumptionMaterials.Save();
        }

        public void Delete(int id)
        {
            _db.ConsumptionMaterials.Delete(id);
            _db.ConsumptionMaterials.Save();
        }
    }
}