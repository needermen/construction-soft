using Cs.Application.Hr.Models;
using Cs.Application.Interfaces;
using Cs.Common.Models;
using Cs.Service.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cs.Service.Admin.Hr
{
    public class WorkerController : ResourceController
    {
        private readonly ICrudOperation<WorkerViewModel> _workers;

        public WorkerController(ICrudOperation<WorkerViewModel> workers)
        {
            _workers = workers;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] Paging paging)
        {
            var result = _workers.Get(paging);
            
            return Json(ServiceResult<ListResult<WorkerViewModel>>.Ok(result));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var workerViewModel = _workers.Get(id);
            if (workerViewModel == null)
                return NotFound();

            return Json(ServiceResult<WorkerViewModel>.Ok(workerViewModel));
        }

        [HttpPost]
        public IActionResult Post([FromBody]WorkerViewModel workerViewModel)
        {
            var id = _workers.Add(workerViewModel);

            return Json(ServiceResult<int>.Ok(id));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]WorkerViewModel workerViewModel)
        {
            _workers.Update(workerViewModel);

            return Json(ServiceResult<bool>.Ok(true));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _workers.Delete(id);

            return Json(ServiceResult<bool>.Ok(true));
        }
    }
}