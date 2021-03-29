using Cs.Application.Interfaces;
using Cs.Common.Models;
using Cs.Domain.Auth;
using Cs.Domain.Technics;
using Cs.Service.Common;
using Cs.Service.Common.Models;
using Cs.Service.Configuration.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Cs.Service.Admin.Technic
{
    public class TechnicCategoryController : ResourceController
    {
        private readonly ICrudOperation<TechnicCategory> _technicCategories;

        public TechnicCategoryController(ICrudOperation<TechnicCategory> technicCategories)
        {
            _technicCategories = technicCategories;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] Paging paging)
        {
            var result = _technicCategories.Get(paging);
            
            return Json(ServiceResult<ListResult<TechnicCategory>>.Ok(result));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var category = _technicCategories.Get(id);
            if (category == null)
                return NotFound();

            return Json(ServiceResult<TechnicCategory>.Ok(category));
        }

        [HttpPost]
        public IActionResult Post([FromBody]TechnicCategory technicCategory)
        {
            var id = _technicCategories.Add(technicCategory);

            return Json(ServiceResult<int>.Ok(id));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]TechnicCategory technicCategory)
        {
            _technicCategories.Update(technicCategory);

            return Json(ServiceResult<bool>.Ok(true));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _technicCategories.Delete(id);

            return Json(ServiceResult<bool>.Ok(true));
        }
    }
}