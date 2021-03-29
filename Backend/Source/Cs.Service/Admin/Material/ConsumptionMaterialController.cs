using Cs.Application.Interfaces;
using Cs.Common.Models;
using Cs.Domain.Materials;
using Cs.Service.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cs.Service.Admin.Material
{
    public class ConsumptionMaterialController : ResourceController
    {
        private readonly ICrudOperation<ConsumptionMaterial> _consumptionMaterials;

        public ConsumptionMaterialController(ICrudOperation<ConsumptionMaterial> consumptionMaterials)
        {
            _consumptionMaterials = consumptionMaterials;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] Paging paging)
        {
            var result = _consumptionMaterials.Get(paging);
            
            return Json(ServiceResult<ListResult<ConsumptionMaterial>>.Ok(result));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var dimension = _consumptionMaterials.Get(id);
            if (dimension == null)
                return NotFound();

            return Json(ServiceResult<ConsumptionMaterial>.Ok(dimension));
        }

        [HttpPost]
        public IActionResult Post([FromBody]ConsumptionMaterial consumptionMaterial)
        {
            var id = _consumptionMaterials.Add(consumptionMaterial);

            return Json(ServiceResult<int>.Ok(id));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]ConsumptionMaterial consumptionMaterial)
        {
            _consumptionMaterials.Update(consumptionMaterial);

            return Json(ServiceResult<bool>.Ok(true));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _consumptionMaterials.Delete(id);

            return Json(ServiceResult<bool>.Ok(true));
        }
    }
}