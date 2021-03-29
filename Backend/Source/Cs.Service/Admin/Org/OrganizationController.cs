using Cs.Application.Interfaces;
using Cs.Application.Org.Models;
using Cs.Common.Models;
using Cs.Service.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cs.Service.Admin.Org
{
    public class OrganizationController : AdminController
    {
        private readonly ICrudOperation<OrganizationViewModel> _organizations;
        
        public OrganizationController(ICrudOperation<OrganizationViewModel> organizations)
        {
            _organizations = organizations;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] Paging paging)
        {
            var result = _organizations.Get(paging);
            
            return Json(ServiceResult<ListResult<OrganizationViewModel>>.Ok(result));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var dimension = _organizations.Get(id);
            if (dimension == null)
                return NotFound();

            return Json(ServiceResult<OrganizationViewModel>.Ok(dimension));
        }

        [HttpPost]
        public IActionResult Post([FromBody]OrganizationViewModel organization)
        {
            var id = _organizations.Add(organization);

            return Json(ServiceResult<int>.Ok(id));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]OrganizationViewModel organization)
        {
            _organizations.Update(organization);

            return Json(ServiceResult<bool>.Ok(true));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _organizations.Delete(id);

            return Json(ServiceResult<bool>.Ok(true));
        }
    }
}