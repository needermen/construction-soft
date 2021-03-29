using Cs.Application.Interfaces;
using Cs.Application.Org.Models;
using Cs.Common.Models;

namespace Cs.Application.Org
{
    public interface IUserService : ICrudOperation<UserViewModel>
    {
        ListResult<UserViewModel> GetExcept(Paging paging, int[] exceptIds);
        bool Exists(string username);
    }
}