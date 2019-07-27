using System.Threading.Tasks;
using VirtueApi.Data.Entities;

namespace VirtueApi.Services.Repositories
{
    public interface IVirtueRepository : IGenericRepository<Virtue>
    {
        Task<bool> Exists(int virtueId);
    }
}