using System.Collections.Generic;
using VirtueApi.Data.Entities;

namespace VirtueApi.Data.Repositories
{
    public interface IEntryRepository : IGenericRepository<Entry>
    {
        IEnumerable<Entry> GetEntriesByVirtueId(int virtueId);
        IEnumerable<Entry> GetEntriesForUser(int userId);
    }
}