using Cs.Common.Models;
using Cs.Domain.Auth;

namespace Cs.Application.Auth
{
    public interface IAuthService
    {
        User Login(string username, string password);
        bool Logout(string token);
        bool ChangePassword(string token, string oldPassword, string newPassword);
        bool IsTokenValid(string token);
        int GetOrganizationId(string token);
        bool HasAtLeastOneRole(string token, int[] roles);
        void AddRoles(int userid, int[] roles);
        ListResult<Role> GetRoles();
        int[] GetRoleIds(string token);
        ListResult<Role> GetRoles(int? organizationId);
    }
}