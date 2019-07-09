using System.Collections.Generic;
using System.Threading.Tasks;

namespace VirtueApi.Repositories
{
    public interface IGenericRepository<TEntity> 
        where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        Task<TEntity> GetById(long id);
        Task Create(TEntity entity);
        Task Update(long id, TEntity entity);
        Task Delete(long id);
    }
}