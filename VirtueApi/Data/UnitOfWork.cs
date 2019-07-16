using System.Threading.Tasks;

namespace VirtueApi.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        
        public UnitOfWork(DataContext context)
        {
            _context = context;
            Virtues = new VirtueRepository(_context);
            Entries = new EntryRepository(_context);
        }
        
        public IVirtueRepository Virtues { get; private set; }
        public IEntryRepository Entries { get; private set; }
        
        public async Task<bool> Complete()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}