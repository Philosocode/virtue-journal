using System.Collections.Generic;
using System.Threading.Tasks;
using VirtueApi.Data.Entities;

namespace VirtueApi.Data.Repositories
{
    public interface IVirtueRepository : IGenericRepository<Virtue>
    {
        Task<bool> VirtueExists(int virtueId);
    }
}