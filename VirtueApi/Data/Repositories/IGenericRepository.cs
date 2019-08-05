using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

// FROM: Mosh Hamedani Repository Tutorial
// https://github.com/chanson5000/Repository-Pattern-with-Csharp-and-Entity-Framework
namespace VirtueApi.Data.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        Task<TEntity> GetByIdAsync(int id);
        IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> FindSingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        Task AddAsync(TEntity entity);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}