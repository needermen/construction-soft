using Cs.Application.Buildings.Models;
using Cs.Application.Interfaces;
using Cs.Common.Models;
using Cs.Service.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cs.Service.CostEstimator.Buildings
{
    [Route("api/work/{workId}/consumptionMaterial")]
    public class WorkConsumptionMaterialController : CostEstimatorController
    {
        private readonly IDependedCrudOperation<WorkConsumptionMaterialViewModel> _workConsumptionMaterials;

        public WorkConsumptionMaterialController(IDependedCrudOperation<WorkConsumptionMaterialViewModel> workConsumptionMaterials)
        {
            _workConsumptionMaterials = workConsumptionMaterials;
        }

        [HttpGet]
        public IActionResult Get(int workId, [FromQuery] Paging paging)
        {
            var result = _workConsumptionMaterials.Get(workId, paging);
            
            return Json(ServiceResult<ListResult<WorkConsumptionMaterialViewModel>>.Ok(result));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int workId, int id)
        {
            var workConsumptionMaterial = _workConsumptionMaterials.Get(workId, id);
            if (workConsumptionMaterial == null)
                return NotFound();

            return Json(ServiceResult<WorkConsumptionMaterialViewModel>.Ok(workConsumptionMaterial));
        }

        [HttpPost]
        public IActionResult Post(int workId, [FromBody]WorkConsumptionMaterialViewModel workConsumptionMaterial)
        {
            var id = _workConsumptionMaterials.Add(workId, workConsumptionMaterial);

            return Json(ServiceResult<int>.Ok(id));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int workId, int id, [FromBody]WorkConsumptionMaterialViewModel workConsumptionMaterial)
        {
            _workConsumptionMaterials.Update(workId, workConsumptionMaterial);

            return Json(ServiceResult<bool>.Ok(true));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int workId, int id)
        {
            _workConsumptionMaterials.Delete(workId, id);

            return Json(ServiceResult<bool>.Ok(true));
        }
    }
}