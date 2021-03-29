using Cs.Application.Buildings.Models;
using Cs.Application.Interfaces;
using Cs.Common.Models;
using Cs.Service.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cs.Service.CostEstimator.Buildings
{
    [Route("api/work/{workId}/buildingMaterial")]
    public class WorkBuildingMaterialController : CostEstimatorController
    {
        private readonly IDependedCrudOperation<WorkBuildingMaterialViewModel> _workBuildingMaterials;

        public WorkBuildingMaterialController(IDependedCrudOperation<WorkBuildingMaterialViewModel> workBuildingMaterials)
        {
            _workBuildingMaterials = workBuildingMaterials;
        }

        [HttpGet]
        public IActionResult Get(int workId, [FromQuery] Paging paging)
        {
            var result = _workBuildingMaterials.Get(workId, paging);
            
            return Json(ServiceResult<ListResult<WorkBuildingMaterialViewModel>>.Ok(result));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int workId, int id)
        {
            var workBuildingMaterial = _workBuildingMaterials.Get(workId, id);
            if (workBuildingMaterial == null)
                return NotFound();

            return Json(ServiceResult<WorkBuildingMaterialViewModel>.Ok(workBuildingMaterial));
        }

        [HttpPost]
        public IActionResult Post(int workId, [FromBody]WorkBuildingMaterialViewModel workBuildingMaterial)
        {
            var id = _workBuildingMaterials.Add(workId, workBuildingMaterial);

            return Json(ServiceResult<int>.Ok(id));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int workId, int id, [FromBody]WorkBuildingMaterialViewModel workBuildingMaterial)
        {
            _workBuildingMaterials.Update(workId, workBuildingMaterial);

            return Json(ServiceResult<bool>.Ok(true));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int workId, int id)
        {
            _workBuildingMaterials.Delete(workId, id);

            return Json(ServiceResult<bool>.Ok(true));
        }
    }
}