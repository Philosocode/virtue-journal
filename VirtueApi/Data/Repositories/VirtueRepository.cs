using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VirtueApi.Data.Entities;

namespace VirtueApi.Data.Repositories
{
    public class VirtueRepository : GenericRepository<Virtue>, IVirtueRepository
    {
        public VirtueRepository(DataContext context) : base(context) {}

        public DataContext DataContext => Context as DataContext;

        public IEnumerable<Virtue> GetVirtuesForUser(int userId)
        {
            return DataContext.Virtues.Where(v => v.UserId == userId).AsEnumerable();
        }

        public Task<bool> Exists(int virtueId)
        {
            return DataContext.Virtues.AnyAsync(v => v.VirtueId == virtueId);
        }

        public Task<bool> BelongsToUser(int virtueId, int userId)
        {
            return DataContext.Virtues
                .AnyAsync(v => v.VirtueId == virtueId && v.UserId == userId);
        }
    }
}