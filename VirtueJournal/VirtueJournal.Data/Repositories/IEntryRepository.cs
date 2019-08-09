using System.Collections.Generic;
using VirtueJournal.Data.Entities;

namespace VirtueJournal.Data.Repositories
{
    public interface IEntryRepository : IGenericRepository<Entry>
    {
        IEnumerable<Entry> GetEntriesByVirtueId(int virtueId);
        IEnumerable<Entry> GetEntriesForUser(int userId);
    }
}