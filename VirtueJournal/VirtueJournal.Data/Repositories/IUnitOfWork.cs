using System.Threading.Tasks;

namespace VirtueJournal.Data.Repositories
{
    public interface IUnitOfWork
    {
        IVirtueRepository Virtues { get; }
        IEntryRepository Entries { get; }
        IAuthRepository Auth { get; }
        Task<bool> Complete();
    }
}