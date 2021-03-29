using Cs.Application.Buildings.Models;
using Cs.Application.Interfaces;
using Cs.Common.Models;
using Cs.Service.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cs.Service.CostEstimator.Buildings
{
    [Route("api/work/{workId}/mainMaterial")]
    public class WorkMainMaterialController : CostEstimatorController
    {
        private readonly IDependedCrudOperation<WorkMainMaterialViewModel> _workMainMaterials;

        public WorkMainMaterialController(IDependedCrudOperation<WorkMainMaterialViewModel> workMainMaterials)
        {
            _workMainMaterials = workMainMaterials;
        }

        [HttpGet]
        public IActionResult Get(int workId, [FromQuery] Paging paging)
        {
            var result = _workMainMaterials.Get(workId, paging);
            
            return Json(ServiceResult<ListResult<WorkMainMaterialViewModel>>.Ok(result));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int workId, int id)
        {
            var workMainMaterial = _workMainMaterials.Get(workId, id);
            if (workMainMaterial == null)
                return NotFound();

            return Json(ServiceResult<WorkMainMaterialViewModel>.Ok(workMainMaterial));
        }

        [HttpPost]
        public IActionResult Post(int workId, [FromBody]WorkMainMaterialViewModel workMainMaterial)
        {
            var id = _workMainMaterials.Add(workId, workMainMaterial);

            return Json(ServiceResult<int>.Ok(id));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int workId, int id, [FromBody]WorkMainMaterialViewModel workMainMaterial)
        {
            _workMainMaterials.Update(workId, workMainMaterial);

            return Json(ServiceResult<bool>.Ok(true));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int workId, int id)
        {
            _workMainMaterials.Delete(workId, id);

            return Json(ServiceResult<bool>.Ok(true));
        }
    }
}