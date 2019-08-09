using System.Threading.Tasks;
using VirtueJournal.Data.Entities;

namespace VirtueJournal.Data.Repositories
{
    public interface IAuthRepository
    {
        Task<User> AuthenticateAsync(string userName, string email, string password);
        Task<User> RegisterAsync(User user, string password);
        Task DeleteAsync(int id);
        void UpdatePassword(User user, string password);
        string GenerateToken(int userId, string secret);
        Task<bool> UserNameInUseAsync(string userName);
        Task<bool> EmailInUseAsync(string email);
        Task<User> GetByIdAsync(int id);
    }
}