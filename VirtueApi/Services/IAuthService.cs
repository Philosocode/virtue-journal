using VirtueApi.Data.Entities;

namespace VirtueApi.Services
{
    public interface IAuthService
    {
        User Authenticate(string username, string password);
        User GetById(int id);
        User Create(User user, string password);
        void Update(User user, string password = null);
        void Delete(int id);
    }
}