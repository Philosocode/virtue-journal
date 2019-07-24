using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VirtueApi.Data.Entities;

namespace VirtueApi.Data.Repositories
{
    public class EntryRepository : GenericRepository<Entry>, IEntryRepository
    {
        public EntryRepository(DataContext context) : base(context) {}

        public DataContext DataContext
        {
            get { return Context as DataContext; }
        }

        public IEnumerable<Entry> GetEntriesByVirtueId(int virtueId)
        {
            return DataContext.VirtueEntries
                .Include(ve => ve.Entry)
                .Where(ve => ve.VirtueId == virtueId)
                .Select(ve => ve.Entry);
            
            // Using Any could be bad for performance
            /*
            return DataContext.Entries
                .Where(e => e.VirtuesLink
                    .Any(v => v.VirtueId == virtueId)
                );
            */
        }
    }
}