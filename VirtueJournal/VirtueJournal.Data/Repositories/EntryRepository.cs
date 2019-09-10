using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VirtueJournal.Data.Entities;

namespace VirtueJournal.Data.Repositories
{
    public class EntryRepository : GenericRepository<Entry>, IEntryRepository
    {
        public EntryRepository(DataContext context) : base(context) {}

        public DataContext DataContext => Context as DataContext;

        public override async Task<Entry> GetByIdAsync(int id)
        {
            return await DataContext.Entries
                .Include(e => e.VirtueLinks)
                .FirstOrDefaultAsync(e => e.EntryId == id);
        }

        public override IEnumerable<Entry> GetAll()
        {
            return DataContext.Entries
                .Include(e => e.VirtueLinks)
                .AsEnumerable();
        }
        
        public IEnumerable<Entry> GetEntriesForUser(int userId)
        {
            return DataContext.Entries
                .Where(e => e.UserId == userId)
                .Include(e => e.VirtueLinks)
                .AsEnumerable();
        }
        
        public async Task<IEnumerable<Entry>> GetUncategorizedEntriesAsync(int userId)
        {
            return await DataContext.Entries
                .Where(e => e.UserId == userId)
                .Where(e => e.VirtueLinks.Count == 0)
                .ToListAsync();
        }

        public IEnumerable<Entry> GetEntriesByVirtueId(int virtueId)
        {
            return DataContext.VirtueEntries
                .Where(ve => ve.VirtueId == virtueId)
                .Include(ve => ve.Entry)
                .Select(ve => ve.Entry)
                .Include(e => e.VirtueLinks)
                .AsEnumerable();
            
            // Using Any could be bad for performance
            /*
            return DataContext.Entries
                .Where(e => e.VirtueLinks
                    .Any(v => v.VirtueId == virtueId)
                );
            */
        }
    }
}