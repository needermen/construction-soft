using Cs.Application.Hr.Models;
using Cs.Application.Interfaces;
using Cs.Common.Models;
using Cs.Service.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cs.Service.Admin.Hr
{
    public class BrigadeController : ResourceController
    {
        private readonly ICrudOperation<BrigadeViewModel> _brigades;

        public BrigadeController(ICrudOperation<BrigadeViewModel> brigades)
        {
            _brigades = brigades;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] Paging paging)
        {
            var result = _brigades.Get(paging);
            
            return Json(ServiceResult<ListResult<BrigadeViewModel>>.Ok(result));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var brigade = _brigades.Get(id);
            if (brigade == null)
                return NotFound();

            return Json(ServiceResult<BrigadeViewModel>.Ok(brigade));
        }

        [HttpPost]
        public IActionResult Post([FromBody]BrigadeViewModel brigade)
        {
            var id = _brigades.Add(brigade);

            return Json(ServiceResult<int>.Ok(id));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]BrigadeViewModel brigade)
        {
            _brigades.Update(brigade);

            return Json(ServiceResult<bool>.Ok(true));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _brigades.Delete(id);

            return Json(ServiceResult<bool>.Ok(true));
        }
    }
}