using Cs.Application.Buildings.Models;
using Cs.Application.Interfaces;
using Cs.Common.Models;
using Cs.Service.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cs.Service.CostEstimator.Buildings
{
    [Route("api/work/{workId}/technic")]
    public class WorkTechnicController : CostEstimatorController
    {
        private readonly IDependedCrudOperation<WorkTechnicViewModel> _workTechnics;

        public WorkTechnicController(IDependedCrudOperation<WorkTechnicViewModel> workTechnics)
        {
            _workTechnics = workTechnics;
        }

        [HttpGet]
        public IActionResult Get(int workId, [FromQuery] Paging paging)
        {
            var result = _workTechnics.Get(workId, paging);
            
            return Json(ServiceResult<ListResult<WorkTechnicViewModel>>.Ok(result));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int workId, int id)
        {
            var workTechnic = _workTechnics.Get(workId, id);
            if (workTechnic == null)
                return NotFound();

            return Json(ServiceResult<WorkTechnicViewModel>.Ok(workTechnic));
        }

        [HttpPost]
        public IActionResult Post(int workId, [FromBody]WorkTechnicViewModel workTechnic)
        {
            var id = _workTechnics.Add(workId, workTechnic);

            return Json(ServiceResult<int>.Ok(id));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int workId, int id, [FromBody]WorkTechnicViewModel workTechnic)
        {
            _workTechnics.Update(workId, workTechnic);

            return Json(ServiceResult<bool>.Ok(true));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int workId, int id)
        {
            _workTechnics.Delete(workId, id);

            return Json(ServiceResult<bool>.Ok(true));
        }
    }
}