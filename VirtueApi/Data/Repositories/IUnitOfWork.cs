using System.Threading.Tasks;

namespace VirtueApi.Data.Repositories
{
    public interface IUnitOfWork
    {
        IVirtueRepository Virtues { get; }
        IEntryRepository Entries { get; }
        Task<bool> Complete();
    }
}