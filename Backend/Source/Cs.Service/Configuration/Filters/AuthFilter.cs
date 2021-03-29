using System;
using System.Linq;
using System.Threading.Tasks;
using Cs.Application.Auth;
using Cs.Domain.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Cs.Service.Configuration.Filters
{   
    public class AuthFilter : Attribute, IAsyncAuthorizationFilter
    {
        private readonly int[] _roles;

        public AuthFilter()
        {
        }

        public AuthFilter(RoleEnums[] roles)
        {
            _roles = roles.Select(it => (int) it).ToArray();
        }

        public Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            using (var scope = context.HttpContext.RequestServices.CreateScope())
            {
                var authService = scope.ServiceProvider.GetRequiredService<IAuthService>();

                var token = context.HttpContext.Request.Headers["Authorization"];

                int.TryParse(context.HttpContext.Request.Headers["OrganizationId"], out var organizationId);

                //TODO remove for release
                //#if DEBUG
                if (token == "12duhiu2u3j2jkjd234")
                {
                    return Task.CompletedTask;
                }
                //#endif
            
                var authorized = _roles == null || !_roles.Any() ? authService.IsTokenValid(token) : authService.HasAtLeastOneRole(token, _roles);
                if (!authorized || organizationId != authService.GetOrganizationId(token))
                {
                    context.Result = new UnauthorizedResult();
                }
            
                return Task.CompletedTask;

            }    
        }
    }
}