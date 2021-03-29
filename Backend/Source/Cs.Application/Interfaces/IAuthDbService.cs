using Cs.Domain.Auth;

namespace Cs.Application.Interfaces
{
    public interface IAuthDbService : IDbService
    {
        IRepository<User> Users { get; set; }
        IRepository<Role> Roles { get; set; }
        IRepository<Organization> Organizations{ get; set; }
    }
}