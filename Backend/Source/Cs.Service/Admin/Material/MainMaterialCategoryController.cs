using Cs.Application.Interfaces;
using Cs.Common.Models;
using Cs.Domain.Materials;
using Cs.Service.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cs.Service.Admin.Material
{
    public class MainMaterialCategoryController : ResourceController
    {
        private readonly ICrudOperation<MainMaterialCategory> _mainMaterialCategories;

        public MainMaterialCategoryController(ICrudOperation<MainMaterialCategory> mainMaterialCategories)
        {
            _mainMaterialCategories = mainMaterialCategories;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] Paging paging)
        {
            var result = _mainMaterialCategories.Get(paging);
            
            return Json(ServiceResult<ListResult<MainMaterialCategory>>.Ok(result));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var category = _mainMaterialCategories.Get(id);
            if (category == null)
                return NotFound();

            return Json(ServiceResult<MainMaterialCategory>.Ok(category));
        }

        [HttpPost]
        public IActionResult Post([FromBody]MainMaterialCategory mainMaterialCategory)
        {
            var id = _mainMaterialCategories.Add(mainMaterialCategory);

            return Json(ServiceResult<int>.Ok(id));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]MainMaterialCategory mainMaterialCategory)
        {
            _mainMaterialCategories.Update(mainMaterialCategory);

            return Json(ServiceResult<bool>.Ok(true));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _mainMaterialCategories.Delete(id);

            return Json(ServiceResult<bool>.Ok(true));
        }
    }
}