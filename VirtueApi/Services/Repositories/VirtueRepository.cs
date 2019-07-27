using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VirtueApi.Data;
using VirtueApi.Data.Entities;

namespace VirtueApi.Services.Repositories
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