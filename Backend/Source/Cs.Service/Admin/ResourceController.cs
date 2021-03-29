using Cs.Domain.Auth;
using Cs.Service.Common;
using Cs.Service.Configuration.Filters;

namespace Cs.Service.Admin
{
    [AuthFilter(new[] { RoleEnums.ResourceManager })]
    public class ResourceController : BaseController
    {
        
    }
}