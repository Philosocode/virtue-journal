using System.Threading.Tasks;
using VirtueApi.Data.Entities;

namespace VirtueApi.Services
{
    public interface IAuthService
    {
        Task<User> AuthenticateAsync(string userName, string password);
        Task<User> GetByIdAsync(int id);
        Task<User> CreateAsync(User user, string password);
        void UpdateAsync(User user, string password = null);
        void DeleteAsync(int id);
        Task<bool> UserNameInUseAsync(string userName);
        Task<bool> EmailInUseAsync(string email);
    }
}