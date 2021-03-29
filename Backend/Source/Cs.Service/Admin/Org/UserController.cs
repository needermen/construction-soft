using Cs.Application.Org;
using Cs.Application.Org.Models;
using Cs.Common.Models;
using Cs.Service.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cs.Service.Admin.Org
{
    public class UserController : AdminController
    {
        private readonly IUserService _users;

        public UserController(IUserService users)
        {
            _users = users;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] Paging paging)
        {
            int.TryParse(HttpContext.Request.Headers["UserId"], out var userId);

            var result = userId > 0 ? _users.GetExcept(paging, new[] {userId}) : _users.Get(paging);

            return Json(ServiceResult<ListResult<UserViewModel>>.Ok(result));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var user = _users.Get(id);
            if (user == null)
                return NotFound();

            return Json(ServiceResult<UserViewModel>.Ok(user));
        }

        [HttpPost]
        public IActionResult Post([FromBody] UserViewModel userViewModel)
        {
            var id = _users.Add(userViewModel);

            return Json(ServiceResult<int>.Ok(id));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UserViewModel userViewModel)
        {
            _users.Update(userViewModel);

            return Json(ServiceResult<bool>.Ok(true));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _users.Delete(id);

            return Json(ServiceResult<bool>.Ok(true));
        }
    }
}