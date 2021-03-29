using Cs.Application.Auth;
using Cs.Application.Org.Models;
using Cs.Common.Models;
using Cs.Service.Auth.Models;
using Cs.Service.Common;
using Cs.Service.Common.Models;
using Cs.Service.Configuration.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Cs.Service.Auth
{
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginModel userLoginModel)
        {
            var user = _authService.Login(userLoginModel.Username, userLoginModel.Password);
            if (user != null)
            {
                var result = new UserModel
                {
                    FullName = user.FullName,
                    Id = user.Id,
                    Token = user.Token,
                    TokenExpireDate = user.TokenExpireDate,
                    OrganizationId = user.OrganizationId,
                    PasswordShouldChange = user.PasswordShouldChange
                };

                result.RoleIds = _authService.GetRoleIds(result.Token);

                return Json(ServiceResult<UserModel>.Ok(result));
            }


            //TODO constant should be variable
            return Json(ServiceResult<UserModel>.Fail("მომხმარებელი ან პაროლი არასწორია"));
        }

        [AuthFilter]
        [HttpPost("logout")]
        public IActionResult Logout(string token)
        {
            //todo validate token

            var result = _authService.Logout(token);

            return Json(ServiceResult<bool>.Ok(result));
        }

        [AuthFilter]
        [HttpPut("changePassword")]
        public IActionResult ChangePassword([FromBody] ChangePasswordModel changePasswordModel)
        {
            //todo validate token

            var result = _authService.ChangePassword(changePasswordModel.Token, changePasswordModel.OldPassword,
                changePasswordModel.NewPassword);

            return Json(ServiceResult<bool>.Ok(result));
        }
    }
}