using Cs.Common.Models;

namespace Cs.Application.Interfaces
{
    public interface IDependedCrudOperation<T>
    {
        ListResult<T> Get(int dependedId, Paging paging);
        T Get(int dependedId, int id);
        int Add(int dependedId, T t);
        void Update(int dependedId, T t);
        void Delete(int dependedId, int id);
    }
}