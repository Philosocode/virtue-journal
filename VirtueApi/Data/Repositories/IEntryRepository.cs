using System.Collections.Generic;
using System.Threading.Tasks;
using VirtueApi.Data.Entities;

namespace VirtueApi.Data.Repositories
{
    public interface IEntryRepository : IGenericRepository<Entry>
    {
        IEnumerable<Entry> GetEntriesByVirtueId(int virtueId);
    }
}