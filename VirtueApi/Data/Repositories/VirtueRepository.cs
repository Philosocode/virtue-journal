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

        public Task<bool> Exists(int virtueId)
        {
            return DataContext.Virtues.AnyAsync(v => v.VirtueId == virtueId);
        }
    }
}