using System.Collections.Generic;
using VirtueApi.Data.Entities;

namespace VirtueApi.Services.Repositories
{
    public interface IEntryRepository : IGenericRepository<Entry>
    {
        IEnumerable<Entry> GetEntriesByVirtueId(int virtueId);
    }
}