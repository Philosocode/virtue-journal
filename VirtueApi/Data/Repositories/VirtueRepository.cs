using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VirtueApi.Data.Entities;

namespace VirtueApi.Data.Repositories
{
    public class VirtueRepository : GenericRepository<Virtue>, IVirtueRepository
    {
        public VirtueRepository(DataContext context) : base(context) {}

        public DataContext DataContext
        {
            get { return Context as DataContext; }
        }

        public Task<bool> VirtueExists(int virtueId)
        {
            return DataContext.Virtues.AnyAsync(v => v.VirtueId == virtueId);
        }
    }
}