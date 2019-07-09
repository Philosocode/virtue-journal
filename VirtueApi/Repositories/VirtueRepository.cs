using VirtueApi.Data;
using VirtueApi.Entities;

namespace VirtueApi.Repositories
{
    public class VirtueRepository : GenericRepository<Virtue>, IVirtueRepository
    {
        public VirtueRepository(DataContext context) : base(context) {}
    }
}