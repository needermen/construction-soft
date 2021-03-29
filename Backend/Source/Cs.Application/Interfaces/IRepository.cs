using System.Linq;

namespace Cs.Application.Interfaces
{
    public interface IRepository<T>
    {
        IQueryable<T> Local();
        IQueryable<T> All();
        T Get(int id);
        IQueryable<T> GetByIds(int[] ids);
        void DeleteFromDb(int id);
        void Delete(int id);
        void Delete(T entity);
        void Add(T entity);
        void AddRange(T[] entities);
        void Update(T entity);
        void Reload(T entity);
        void Attach(T entity);
        void Save();
        bool Changed();
    }
}