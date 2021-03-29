using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Cs.Application.Interfaces;
using Cs.Application.Org.Models;
using Cs.Application.Tools;
using Cs.Common.Models;
using Cs.Common.Tools.Security;
using Cs.Domain.Auth;
using Microsoft.EntityFrameworkCore;

namespace Cs.Application.Org
{
    public class UserService : IUserService
    {
        private readonly IAuthDbService _db;
        private readonly IMapper _mapper;

        public UserService(IAuthDbService db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public ListResult<UserViewModel> GetExcept(Paging paging, int[] exceptIds)
        {
            var users = Get(paging);
            users.Items = users.Items.Where(u => !exceptIds.Contains(u.Id)).ToList();

            return users;
        }

        public bool Exists(string username)
        {
            return _db.Users.All().Any(bc => bc.UserName == username);
        }

        public ListResult<UserViewModel> Get(Paging paging)
        {
            var users = _db.Users.All();
            if (!string.IsNullOrEmpty(paging.Search))
                users = users.Where(t => t.FullName.Contains(paging.Search)
                                         || t.PhoneNumber.Contains(paging.Search)
                                         || t.UserName.Contains(paging.Search)
                                         || t.PersonalId.Contains(paging.Search)
                                         || t.Organization == null || t.Organization.Name.Contains(paging.Search));

            var result = users.Include(u => u.Organization).Include(u => u.Roles).Skip(paging.Skip).Take(paging.Count)
                .ToList();

            var userViewModels = _mapper.Map<List<UserViewModel>>(result);

            var total = users.Count();

            return new ListResult<UserViewModel>(userViewModels, total);
        }

        public UserViewModel Get(int id)
        {
            var user = _db.Users.All().Include(org => org.Roles).FirstOrDefault(org => org.Id == id);
            if (user == null)
                throw new ServiceException(UiConstants.ObjectNotFound);

            return _mapper.Map<UserViewModel>(user);
        }

        public int Add(UserViewModel userViewModel)
        {
            if (Exists(userViewModel.UserName))
                throw new ServiceException(UiConstants.EntityWithSameNameAlreadyExists(userViewModel.UserName));

            var hashedPassword = CryptoHelper.HashPassword("123!@#");
            var newUser = new User
            {
                PasswordHash = hashedPassword.Hash,
                PasswordSalt = hashedPassword.Salt
            };

            _mapper.Map(userViewModel, newUser);

            _db.Users.Add(newUser);
            _db.Users.Save();

            return newUser.Id;
        }

        public void Update(UserViewModel userViewModel)
        {
            if (_db.Users.All().Any(bc => bc.UserName == userViewModel.UserName && bc.Id != userViewModel.Id))
                throw new ServiceException(UiConstants.EntityWithSameNameAlreadyExists(userViewModel.UserName));

            var userToUpdate = _db.Users.All().Include(u => u.Roles).FirstOrDefault(u => u.Id == userViewModel.Id);
            if (userToUpdate != null)
            {
                _mapper.Map(userViewModel, userToUpdate);

                _db.Users.Update(userToUpdate);
                _db.Users.Save();
            }
        }

        public void Delete(int id)
        {
            _db.Users.Delete(id);
            _db.Users.Save();
        }
    }
}