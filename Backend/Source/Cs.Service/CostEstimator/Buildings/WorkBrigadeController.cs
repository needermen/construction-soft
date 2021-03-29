using Cs.Application.Buildings.Models;
using Cs.Application.Interfaces;
using Cs.Common.Models;
using Cs.Service.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cs.Service.CostEstimator.Buildings
{
    [Route("api/work/{workId}/brigade")]
    public class WorkBrigadeController : CostEstimatorController
    {
        private readonly IDependedCrudOperation<WorkBrigadeViewModel> _workBrigades;

        public WorkBrigadeController(IDependedCrudOperation<WorkBrigadeViewModel> workBrigades)
        {
            _workBrigades = workBrigades;
        }

        [HttpGet]
        public IActionResult Get(int workId, [FromQuery] Paging paging)
        {
            var result = _workBrigades.Get(workId, paging);
            
            return Json(ServiceResult<ListResult<WorkBrigadeViewModel>>.Ok(result));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int workId, int id)
        {
            var workBrigade = _workBrigades.Get(workId, id);
            if (workBrigade == null)
                return NotFound();

            return Json(ServiceResult<WorkBrigadeViewModel>.Ok(workBrigade));
        }

        [HttpPost]
        public IActionResult Post(int workId, [FromBody]WorkBrigadeViewModel workBrigade)
        {
            var id = _workBrigades.Add(workId, workBrigade);

            return Json(ServiceResult<int>.Ok(id));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int workId, int id, [FromBody]WorkBrigadeViewModel workBrigade)
        {
            _workBrigades.Update(workId, workBrigade);

            return Json(ServiceResult<bool>.Ok(true));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int workId, int id)
        {
            _workBrigades.Delete(workId, id);

            return Json(ServiceResult<bool>.Ok(true));
        }
    }
}