using Cs.Application.Interfaces;
using Cs.Common.Models;
using Cs.Domain.Materials;
using Cs.Service.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cs.Service.Admin.Material
{
    public class BuildingMaterialCategoryController : ResourceController
    {
        private readonly ICrudOperation<BuildingMaterialCategory> _buildingMaterialCategories;

        public BuildingMaterialCategoryController(ICrudOperation<BuildingMaterialCategory> buildingMaterialCategories)
        {
            _buildingMaterialCategories = buildingMaterialCategories;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] Paging paging)
        {
            var result = _buildingMaterialCategories.Get(paging);
            
            return Json(ServiceResult<ListResult<BuildingMaterialCategory>>.Ok(result));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var category = _buildingMaterialCategories.Get(id);
            if (category == null)
                return NotFound();

            return Json(ServiceResult<BuildingMaterialCategory>.Ok(category));
        }

        [HttpPost]
        public IActionResult Post([FromBody]BuildingMaterialCategory buildingMaterialCategory)
        {
            var id = _buildingMaterialCategories.Add(buildingMaterialCategory);

            return Json(ServiceResult<int>.Ok(id));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]BuildingMaterialCategory buildingMaterialCategory)
        {
            _buildingMaterialCategories.Update(buildingMaterialCategory);

            return Json(ServiceResult<bool>.Ok(true));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _buildingMaterialCategories.Delete(id);

            return Json(ServiceResult<bool>.Ok(true));
        }
    }
}