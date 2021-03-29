using Cs.Common.Models;

namespace Cs.Application.Interfaces
{
    public interface ICrudOperation<T>
    {
        ListResult<T> Get(Paging paging);
        T Get(int id);
        int Add(T t);
        void Update(T t);
        void Delete(int id);
    }
}