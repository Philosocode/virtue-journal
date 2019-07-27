using System.Threading.Tasks;

namespace VirtueApi.Services.Repositories
{
    public interface IUnitOfWork
    {
        IVirtueRepository Virtues { get; }
        IEntryRepository Entries { get; }
        IAuthService Auth { get; }
        Task<bool> Complete();
    }
}