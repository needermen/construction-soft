using Cs.Application.Interfaces;
using Cs.Common.Models;
using Cs.Domain.Materials;
using Cs.Service.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cs.Service.Admin.Material
{
    public class MaterialDimensionController : ResourceController
    { 
        private readonly ICrudOperation<MaterialDimension> _materialDimensions;

        public MaterialDimensionController(ICrudOperation<MaterialDimension> materialDimensions)
        {
            _materialDimensions = materialDimensions;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] Paging paging)
        {
            var result = _materialDimensions.Get(paging);
            
            return Json(ServiceResult<ListResult<MaterialDimension>>.Ok(result));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var dimension = _materialDimensions.Get(id);
            if (dimension == null)
                return NotFound();

            return Json(ServiceResult<MaterialDimension>.Ok(dimension));
        }

        [HttpPost]
        public IActionResult Post([FromBody]MaterialDimension materialDimension)
        {
            var id = _materialDimensions.Add(materialDimension);

            return Json(ServiceResult<int>.Ok(id));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]MaterialDimension materialDimension)
        {
            _materialDimensions.Update(materialDimension);

            return Json(ServiceResult<bool>.Ok(true));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _materialDimensions.Delete(id);

            return Json(ServiceResult<bool>.Ok(true));
        }
    }
}