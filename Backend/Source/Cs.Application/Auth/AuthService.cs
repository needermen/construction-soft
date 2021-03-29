using System;
using System.Linq;
using Cs.Application.Interfaces;
using Cs.Application.Tools;
using Cs.Common.Models;
using Cs.Domain.Auth;
using Cs.Common.Tools;
using Cs.Common.Tools.Security;
using Microsoft.EntityFrameworkCore;

namespace Cs.Application.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IAuthDbService _db;

        public AuthService(IAuthDbService db)
        {
            _db = db;
        }

        public User Login(string username, string password)
        {
            var user = _db.Users.All().Include(u => u.Organization).FirstOrDefault(u => u.UserName == username);
            if (user == null)
                return null;

            //TODO constant should be variable
            if (!user.Active)
                throw new ServiceException("მომხმარებელი არ არის აქტიური, მიმართეთ ადმინისტრატორს");

            //TODO constant should be variable
            if (user.Organization != null && !user.Organization.Active)
                throw new ServiceException("მომხმარებლის ორგანიზაცია არ არის აქტიური, მიმართეთ ადმინისტრატორს");

            if (CryptoHelper.CheckPassword(password, user.PasswordSalt, user.PasswordHash))
            {
                user.Token = TokenGenerator.Generate();
                user.TokenExpireDate = DateTime.Now.AddMinutes(20);

                _db.Users.Update(user);
                _db.Users.Save();

                return user;
            }

            return null;
        }

        public bool Logout(string token)
        {
            var user = _db.Users.All().FirstOrDefault(u => u.Token == token);
            if (user != null)
            {
                user.Token = null;
                user.TokenExpireDate = DateTime.Now;

                _db.Users.Update(user);
                _db.Users.Save();
            }

            return true;
        }

        public bool ChangePassword(string token, string oldPassword, string newPassword)
        {
            var user = _db.Users.All().FirstOrDefault(u => u.Token == token);
            if (user == null)
                throw new ServiceException(UiConstants.ObjectNotFound);

            if (CryptoHelper.CheckPassword(oldPassword, user.PasswordSalt, user.PasswordHash))
            {
                var hashedPassword = CryptoHelper.HashPassword(newPassword);

                user.PasswordHash = hashedPassword.Hash;
                user.PasswordSalt = hashedPassword.Salt;
                user.PasswordShouldChange = false;

                _db.Users.Update(user);
                _db.Users.Save();

                return true;
            }

            throw new ServiceException("პაროლი არასწორია");
        }

        public bool IsTokenValid(string token)
        {
            var user = _db.Users.All().FirstOrDefault(u => u.Token == token && u.TokenExpireDate > DateTime.Now);
            if (user != null)
            {
                UpdateTokenExpireDate(user);
                return true;
            }

            return false;
        }

        public int GetOrganizationId(string token)
        {
            var user = _db.Users.All().FirstOrDefault(u => u.Token == token && u.TokenExpireDate > DateTime.Now);
            if (user == null)
            {
                throw new ServiceException(UiConstants.ObjectNotFound);
            }

            return user.OrganizationId ?? 0;
        }

        public bool HasAtLeastOneRole(string token, int[] roles)
        {
            var user = _db.Users.All().Include(u => u.Roles)
                .FirstOrDefault(u => u.Token == token && u.TokenExpireDate > DateTime.Now);
            if (user != null)
            {
                var hasRole = user.Roles.Any(r => roles.Contains(r.RoleId));
                if (hasRole)
                {
                    UpdateTokenExpireDate(user);
                    return true;
                }
            }

            return false;
        }

        public void AddRoles(int userid, int[] roleids)
        {
            var user = _db.Users.All().Include(u => u.Roles).FirstOrDefault(it => it.Id == userid);
            if (user != null)
            {
                foreach (var roleId in roleids)
                {
                    var role = _db.Roles.Get(roleId);
                    if (role != null)
                    {
                        user.Roles.Add(new UserRole
                        {
                            Role = role
                        });
                    }
                }

                _db.Users.Update(user);
                _db.Users.Save();
            }
        }

        public ListResult<Role> GetRoles()
        {
            var roles = _db.Roles.All();

            return new ListResult<Role>(roles.ToList(), roles.Count());
        }

        public int[] GetRoleIds(string token)
        {
            var user = _db.Users.All()
                .Include(u => u.Roles)
                .FirstOrDefault(u => u.Token == token);
            if (user == null)
                throw new ServiceException(UiConstants.ObjectNotFound);

            return user.Roles.Select(r => r.RoleId).ToArray();
        }

        public ListResult<Role> GetRoles(int? organizationId)
        {
            if (organizationId.HasValue)
            {
                var org = _db.Organizations.All()
                    .Include(o => o.Roles)
                    .ThenInclude(r => r.Role)
                    .FirstOrDefault(o => o.Id == organizationId);

                if (org != null)
                {
                    return new ListResult<Role>(org.Roles.Select(r => r.Role).ToList(), org.Roles.Count);
                }
            }

            var adminRole = _db.Roles.All().Where(r => r.Id == (int) RoleEnums.Admin).ToList();

            return new ListResult<Role>(adminRole, adminRole.Count());
        }

        private void UpdateTokenExpireDate(User user)
        {
            user.TokenExpireDate = DateTime.Now.AddMinutes(30);
            _db.Users.Update(user);
            _db.Users.Save();
        }
    }
}