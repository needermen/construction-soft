using Cs.Application.Interfaces;
using Cs.Common.Models;
using Cs.Domain.Materials;
using Cs.Service.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cs.Service.Admin.Material
{
    public class MainMaterialController : ResourceController
    {
        private readonly ICrudOperation<MainMaterial> _mainMaterials;

        public MainMaterialController(ICrudOperation<MainMaterial> mainMaterials)
        {
            _mainMaterials = mainMaterials;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] Paging paging)
        {
            var result = _mainMaterials.Get(paging);
            
            return Json(ServiceResult<ListResult<MainMaterial>>.Ok(result));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var dimension = _mainMaterials.Get(id);
            if (dimension == null)
                return NotFound();

            return Json(ServiceResult<MainMaterial>.Ok(dimension));
        }

        [HttpPost]
        public IActionResult Post([FromBody]MainMaterial mainMaterial)
        {
            var id = _mainMaterials.Add(mainMaterial);

            return Json(ServiceResult<int>.Ok(id));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]MainMaterial mainMaterial)
        {
            _mainMaterials.Update(mainMaterial);

            return Json(ServiceResult<bool>.Ok(true));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _mainMaterials.Delete(id);

            return Json(ServiceResult<bool>.Ok(true));
        }
    }
}