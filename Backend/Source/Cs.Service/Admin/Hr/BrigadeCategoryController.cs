using Cs.Application.Interfaces;
using Cs.Common.Models;
using Cs.Domain.Hr;
using Cs.Service.Common;
using Cs.Service.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cs.Service.Admin.Hr
{
    public class BrigadeCategoryController : ResourceController
    {
        private readonly ICrudOperation<BrigadeCategory> _brigadeCategories;

        public BrigadeCategoryController(ICrudOperation<BrigadeCategory> brigadeCategories)
        {
            _brigadeCategories = brigadeCategories;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] Paging paging)
        {
            var result = _brigadeCategories.Get(paging);
            
            return Json(ServiceResult<ListResult<BrigadeCategory>>.Ok(result));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var category = _brigadeCategories.Get(id);
            if (category == null)
                return NotFound();

            return Json(ServiceResult<BrigadeCategory>.Ok(category));
        }

        [HttpPost]
        public IActionResult Post([FromBody]BrigadeCategory brigadeCategory)
        {
            var id = _brigadeCategories.Add(brigadeCategory);

            return Json(ServiceResult<int>.Ok(id));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]BrigadeCategory brigadeCategory)
        {
            _brigadeCategories.Update(brigadeCategory);

            return Json(ServiceResult<bool>.Ok(true));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _brigadeCategories.Delete(id);

            return Json(ServiceResult<bool>.Ok(true));
        }
    }
}