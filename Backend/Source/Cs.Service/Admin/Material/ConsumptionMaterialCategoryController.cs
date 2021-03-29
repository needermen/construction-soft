using Cs.Application.Interfaces;
using Cs.Common.Models;
using Cs.Domain.Materials;
using Cs.Service.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cs.Service.Admin.Material
{
    public class ConsumptionMaterialCategoryController : ResourceController
    {
        private readonly ICrudOperation<ConsumptionMaterialCategory> _consumptionMaterialCategories;

        public ConsumptionMaterialCategoryController(ICrudOperation<ConsumptionMaterialCategory> consumptionMaterialCategories)
        {
            _consumptionMaterialCategories = consumptionMaterialCategories;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] Paging paging)
        {
            var result = _consumptionMaterialCategories.Get(paging);
            
            return Json(ServiceResult<ListResult<ConsumptionMaterialCategory>>.Ok(result));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var category = _consumptionMaterialCategories.Get(id);
            if (category == null)
                return NotFound();

            return Json(ServiceResult<ConsumptionMaterialCategory>.Ok(category));
        }

        [HttpPost]
        public IActionResult Post([FromBody]ConsumptionMaterialCategory consumptionMaterialCategory)
        {
            var id = _consumptionMaterialCategories.Add(consumptionMaterialCategory);

            return Json(ServiceResult<int>.Ok(id));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]ConsumptionMaterialCategory consumptionMaterialCategory)
        {
            _consumptionMaterialCategories.Update(consumptionMaterialCategory);

            return Json(ServiceResult<bool>.Ok(true));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _consumptionMaterialCategories.Delete(id);

            return Json(ServiceResult<bool>.Ok(true));
        }
    }
}