using System.Collections.Generic;
using System.Threading.Tasks;
using VirtueJournal.Data.Entities;

namespace VirtueJournal.Data.Repositories
{
    public interface IVirtueRepository : IGenericRepository<Virtue>
    {
        Task<IEnumerable<Virtue>> GetVirtuesForUserAsync(int userId);
        Task<bool> Exists(int virtueId);
        Task<bool> BelongsToUser(int virtueId, int userId);
    }
}