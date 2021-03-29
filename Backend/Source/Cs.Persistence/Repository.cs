using System;
using System.Linq;
using Cs.Application.Interfaces;
using Cs.Domain;
using Microsoft.EntityFrameworkCore;

namespace Cs.Persistence
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly DbContext _dbContext;
        private readonly IQueryable<T> _entities;
        private readonly IQueryable<T> _local;

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _entities = _dbContext.Set<T>().Where(it => it.EntityStatus == EntityStatus.Active)
                .OrderByDescending(it => it.CreatedDate);
            _local = _dbContext.Set<T>().Local.Where(it => it.EntityStatus == EntityStatus.Active)
                .OrderByDescending(it => it.CreatedDate).AsQueryable();
        }

        public IQueryable<T> Local()
        {
            return _local;
        }

        public IQueryable<T> All()
        {
            return _entities;
        }

        public T Get(int id)
        {
            return _entities.FirstOrDefault(it => it.Id == id);
        }

        public IQueryable<T> GetByIds(int[] ids)
        {
            return _entities.Where(it => ids.Contains(it.Id));
        }

        public void Delete(int id)
        {
            var entity = Get(id);
            entity.EntityStatus = EntityStatus.Deleted;
        }

        public void DeleteFromDb(int id)
        {
            var entity = Get(id);
            _dbContext.Remove(entity);
        }

        public void Delete(T entity)
        {
            _dbContext.Remove(entity);
        }

        public void Add(T entity)
        {
            entity.CreatedDate = DateTime.UtcNow;
            _dbContext.Add(entity);
        }

        public void AddRange(T[] entities)
        {
            foreach (var entity in entities)
            {
                Add(entity);
            }
        }

        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Reload(T entity)
        {
            _dbContext.Entry(entity).Reload();
        }

        public void Attach(T entity)
        {
            _dbContext.Attach(entity);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public bool Changed()
        {
            return _dbContext.ChangeTracker.HasChanges();
        }
    }
}