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
    }
}