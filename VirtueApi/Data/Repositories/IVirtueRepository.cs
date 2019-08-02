using System.Collections.Generic;
using System.Threading.Tasks;
using VirtueApi.Data.Entities;

namespace VirtueApi.Data.Repositories
{
    public interface IVirtueRepository : IGenericRepository<Virtue>
    {
        IEnumerable<Virtue> GetVirtuesForUser(int userId);
        Task<bool> Exists(int virtueId);
        Task<bool> BelongsToUser(int virtueId, int userId);
    }
}