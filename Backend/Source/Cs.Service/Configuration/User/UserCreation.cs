using System.Collections.Generic;
using Cs.Application.Org;
using Cs.Application.Org.Models;
using Cs.Domain.Auth;
using Microsoft.Extensions.DependencyInjection;

namespace Cs.Service.Configuration.User
{
    public static class UserCreation
    {
        public static IServiceCollection CreateUserAtStartUp(this IServiceCollection services)
        {
            var provider = services.BuildServiceProvider();

            var userService = provider.GetService<IUserService>();

            var roles = new List<int> {(int) RoleEnums.Admin, (int) RoleEnums.ResourceManager};

            var username = "admin";
            if (!userService.Exists(username))
            {
                var userToCreate = new UserViewModel
                {
                    FullName = "super admin",
                    UserName = username,
                    RoleIds = roles.ToArray()
                };

                userService.Add(userToCreate);
            }

            return services;
        }
    }
}