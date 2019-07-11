using VirtueApi.Entities;

namespace VirtueApi.Data
{
    public class EntryRepository : GenericRepository<Entry>, IEntryRepository
    {
        public EntryRepository(DataContext context) : base(context) {}
    }
}