using Cs.Application.Interfaces;
using Cs.Common.Models;
using Cs.Domain.Hr;
using Cs.Service.Common;
using Cs.Service.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cs.Service.Admin.Hr
{
    public class WorkerCategoryController : ResourceController
    {
        private readonly ICrudOperation<WorkerCategory> _workerCategories;

        public WorkerCategoryController(ICrudOperation<WorkerCategory> workerCategories)
        {
            _workerCategories = workerCategories;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] Paging paging)
        {
            var result = _workerCategories.Get(paging);
            
            return Json(ServiceResult<ListResult<WorkerCategory>>.Ok(result));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var category = _workerCategories.Get(id);
            if (category == null)
                return NotFound();

            return Json(ServiceResult<WorkerCategory>.Ok(category));
        }

        [HttpPost]
        public IActionResult Post([FromBody]WorkerCategory workerCategory)
        {
            var id = _workerCategories.Add(workerCategory);

            return Json(ServiceResult<int>.Ok(id));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]WorkerCategory workerCategory)
        {
            _workerCategories.Update(workerCategory);

            return Json(ServiceResult<bool>.Ok(true));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _workerCategories.Delete(id);

            return Json(ServiceResult<bool>.Ok(true));
        }
    }
}