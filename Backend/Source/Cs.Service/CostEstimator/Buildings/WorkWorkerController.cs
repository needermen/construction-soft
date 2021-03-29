using Cs.Application.Buildings.Models;
using Cs.Application.Interfaces;
using Cs.Common.Models;
using Cs.Service.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cs.Service.CostEstimator.Buildings
{
    [Route("api/work/{workId}/worker")]
    public class WorkWorkerController : CostEstimatorController
    {
        private readonly IDependedCrudOperation<WorkWorkerViewModel> _workWorkers;

        public WorkWorkerController(IDependedCrudOperation<WorkWorkerViewModel> workWorkers)
        {
            _workWorkers = workWorkers;
        }

        [HttpGet]
        public IActionResult Get(int workId, [FromQuery] Paging paging)
        {
            var result = _workWorkers.Get(workId, paging);
            
            return Json(ServiceResult<ListResult<WorkWorkerViewModel>>.Ok(result));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int workId, int id)
        {
            var workWorker = _workWorkers.Get(workId, id);
            if (workWorker == null)
                return NotFound();

            return Json(ServiceResult<WorkWorkerViewModel>.Ok(workWorker));
        }

        [HttpPost]
        public IActionResult Post(int workId, [FromBody]WorkWorkerViewModel workWorker)
        {
            var id = _workWorkers.Add(workId, workWorker);

            return Json(ServiceResult<int>.Ok(id));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int workId, int id, [FromBody]WorkWorkerViewModel workWorker)
        {
            _workWorkers.Update(workId, workWorker);

            return Json(ServiceResult<bool>.Ok(true));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int workId, int id)
        {
            _workWorkers.Delete(workId, id);

            return Json(ServiceResult<bool>.Ok(true));
        }
    }
}