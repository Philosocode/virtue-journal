using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using VirtueApi.Entities;

namespace VirtueApi.Data
{
    public class EntryRepository : GenericRepository<Entry>, IEntryRepository
    {
        public EntryRepository(DataContext context) : base(context) {}

        public DataContext DataContext
        {
            get { return Context as DataContext; }
        }
    }
}