using Cs.Application.Interfaces;
using Cs.Common.Models;
using Cs.Service.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cs.Service.Admin.Technic
{
    public class TechnicController : ResourceController
    {
        private readonly ICrudOperation<Domain.Technics.Technic> _technics;

        public TechnicController(ICrudOperation<Domain.Technics.Technic> technics)
        {
            _technics = technics;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] Paging paging)
        {
            var result = _technics.Get(paging);
            
            return Json(ServiceResult<ListResult<Domain.Technics.Technic>>.Ok(result));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var dimension = _technics.Get(id);
            if (dimension == null)
                return NotFound();

            return Json(ServiceResult<Domain.Technics.Technic>.Ok(dimension));
        }

        [HttpPost]
        public IActionResult Post([FromBody]Domain.Technics.Technic technic)
        {
            var id = _technics.Add(technic);

            return Json(ServiceResult<int>.Ok(id));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Domain.Technics.Technic technic)
        {
            _technics.Update(technic);

            return Json(ServiceResult<bool>.Ok(true));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _technics.Delete(id);

            return Json(ServiceResult<bool>.Ok(true));
        }
    }
}