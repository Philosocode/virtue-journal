using VirtueApi.Entities;

namespace VirtueApi.Data
{
    public class VirtueRepository : GenericRepository<Virtue>, IVirtueRepository
    {
        public VirtueRepository(DataContext context) : base(context) {}
    }
}