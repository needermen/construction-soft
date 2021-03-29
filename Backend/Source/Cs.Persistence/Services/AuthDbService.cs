using System.Transactions;
using Cs.Application.Interfaces;
using Cs.Domain.Auth;

namespace Cs.Persistence.Services
{
    public class AuthDbService : IAuthDbService
    {
        public IRepository<User> Users { get; set; }
        public IRepository<Role> Roles { get; set; }
        public IRepository<Organization> Organizations{ get; set; }

        public AuthDbService(IRepository<User> users, IRepository<Role> roles, IRepository<Organization> organizations)
        {
            Users = users;
            Roles = roles;
            Organizations = organizations;
        }

        public void Save()
        {
            using (var scope = new TransactionScope())
            {
                if (Users.Changed())
                    Users.Save();

                if (Roles.Changed())
                    Roles.Save();
                
                if(Organizations.Changed())
                    Organizations.Save();
                    
                scope.Complete();
            }
        }
    }
}