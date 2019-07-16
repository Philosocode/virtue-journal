using System.Collections.Generic;
using System.Linq;
using VirtueApi.Entities;

namespace VirtueApi.Data
{
    public class VirtueRepository : GenericRepository<Virtue>, IVirtueRepository
    {
        public VirtueRepository(DataContext context) : base(context) {}

        public DataContext DataContext
        {
            get { return Context as DataContext; }
        }
    }
}