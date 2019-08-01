using System.Collections.Generic;
using VirtueApi.Data.Entities;

namespace VirtueApi.Services.Repositories
{
    public interface IEntryRepository : IGenericRepository<Entry>
    {
        IEnumerable<Entry> GetEntriesByVirtueId(int virtueId);
        IEnumerable<Entry> GetAllEntriesForUser(int userId);
    }
}