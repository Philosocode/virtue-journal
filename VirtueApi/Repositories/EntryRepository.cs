using VirtueApi.Data;
using VirtueApi.Entities;

namespace VirtueApi.Repositories
{
    public class EntryRepository : GenericRepository<Entry>, IEntryRepository
    {
        public EntryRepository(DataContext context) : base(context) {}
    }
}