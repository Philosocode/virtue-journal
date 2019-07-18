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
            var virtue = DataContext
                .Virtues
                .Include("EntriesLink")
                .FirstOrDefaultAsync(v => v.VirtueId == virtueId)
                .Result;

            var entries = virtue.EntriesLink.Select(e => e.Entry);

            return entries;
        }
    }
}