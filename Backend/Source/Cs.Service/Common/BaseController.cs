using Cs.Application.Tools;
using Microsoft.AspNetCore.Mvc;

namespace Cs.Service.Common
{
    [Route("api/[controller]")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class BaseController : Controller
    {
        protected int GetOrgId()
        {
            int.TryParse(HttpContext.Request.Headers["OrganizationId"], out var orgId);
            return orgId;
        }
        
        protected int ValidateAndGetOrgId()
        {
            var orgId = GetOrgId();
            if (orgId <= 0)
                throw new ServiceException(UiConstants.UnauthorizedAccess);
            return orgId;
        }
    }
}