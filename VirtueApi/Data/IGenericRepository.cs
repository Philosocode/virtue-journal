using System.Collections.Generic;
using System.Threading.Tasks;

namespace VirtueApi.Data
{
    public interface IGenericRepository<TEntity> 
        where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        Task<TEntity> GetById(long id);
        Task Create(TEntity entity);
        Task<bool> Exists(long id);
        Task Update(TEntity entity);
        Task Delete(long id);
    }
}