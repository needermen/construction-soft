using Cs.Application.Interfaces;
using Cs.Common.Models;
using Cs.Domain.Technics;
using Cs.Service.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cs.Service.Admin.Technic
{
    public class TechnicDimensionController : ResourceController
    {
        private readonly ICrudOperation<TechnicDimension> _technicDimensions;

        public TechnicDimensionController(ICrudOperation<TechnicDimension> technicDimensions)
        {
            _technicDimensions = technicDimensions;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] Paging paging)
        {
            var result = _technicDimensions.Get(paging);
            
            return Json(ServiceResult<ListResult<TechnicDimension>>.Ok(result));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var dimension = _technicDimensions.Get(id);
            if (dimension == null)
                return NotFound();

            return Json(ServiceResult<TechnicDimension>.Ok(dimension));
        }

        [HttpPost]
        public IActionResult Post([FromBody]TechnicDimension technicDimension)
        {
            var id = _technicDimensions.Add(technicDimension);

            return Json(ServiceResult<int>.Ok(id));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]TechnicDimension technicDimension)
        {
            _technicDimensions.Update(technicDimension);

            return Json(ServiceResult<bool>.Ok(true));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _technicDimensions.Delete(id);

            return Json(ServiceResult<bool>.Ok(true));
        }
    }
}