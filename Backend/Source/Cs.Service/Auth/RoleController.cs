using Cs.Application.Auth;
using Cs.Common.Models;
using Cs.Domain.Auth;
using Cs.Service.Common;
using Cs.Service.Common.Models;
using Cs.Service.Configuration.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Cs.Service.Auth
{
    [AuthFilter]
    public class RoleController : BaseController
    {
        private readonly IAuthService _authService;

        public RoleController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _authService.GetRoles();
            
            return Json(ServiceResult<ListResult<Role>>.Ok(result));
        }
        
        [HttpGet("Org")]
        public IActionResult Get(int? organizationId)
        {
            var result = _authService.GetRoles(organizationId);
            
            return Json(ServiceResult<ListResult<Role>>.Ok(result));
        }
    }
}