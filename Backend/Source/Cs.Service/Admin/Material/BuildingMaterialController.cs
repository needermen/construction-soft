using Cs.Application.Interfaces;
using Cs.Common.Models;
using Cs.Domain.Materials;
using Cs.Service.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cs.Service.Admin.Material
{
    public class BuildingMaterialController : ResourceController
    {
        private readonly ICrudOperation<BuildingMaterial> _buildingMaterials;

        public BuildingMaterialController(ICrudOperation<BuildingMaterial> buildingMaterials)
        {
            _buildingMaterials = buildingMaterials;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] Paging paging)
        {
            var result = _buildingMaterials.Get(paging);
            
            return Json(ServiceResult<ListResult<BuildingMaterial>>.Ok(result));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var dimension = _buildingMaterials.Get(id);
            if (dimension == null)
                return NotFound();

            return Json(ServiceResult<BuildingMaterial>.Ok(dimension));
        }

        [HttpPost]
        public IActionResult Post([FromBody]BuildingMaterial buildingMaterial)
        {
            var id = _buildingMaterials.Add(buildingMaterial);

            return Json(ServiceResult<int>.Ok(id));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]BuildingMaterial buildingMaterial)
        {
            _buildingMaterials.Update(buildingMaterial);

            return Json(ServiceResult<bool>.Ok(true));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _buildingMaterials.Delete(id);

            return Json(ServiceResult<bool>.Ok(true));
        }
    }
}