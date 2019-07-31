using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VirtueApi.Data;
using VirtueApi.Data.Entities;
using VirtueApi.Shared;

namespace VirtueApi.Services
{
    // FROM: https://jasonwatmore.com/post/2018/06/26/aspnet-core-21-simple-api-for-authentication-registration-and-user-management
    public class AuthService : IAuthService
    {
        private readonly DataContext _context;

        public AuthService(DataContext context)
        {
            _context = context;
        }
        
        public async Task<User> AuthenticateAsync(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                return null;

            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == userName);

            // check if user exists
            if (user == null)
                return null;

            // check if password is correct
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            // authentication successful
            return user;
        }
        
        public Task<User> GetByIdAsync(int id)
        {
            return _context.Users.FindAsync(id);
        }
        
        public async Task<User> CreateAsync(User user, string password)
        {
            CreatePasswordHash(password, out var passwordHash, out var passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }
        
        public async void UpdateAsync(User userData, string password = null)
        {
            var user = await _context.Users.FindAsync(userData.UserId);

            if (user == null)
                throw new AppException("User not found");

            if (userData.UserName != user.UserName)
            {
                // UserName has changed so check if the new UserName is already taken
                if (_context.Users.Any(x => x.UserName == userData.UserName))
                    throw new AppException($"Username {userData.UserName} is already taken.");
            }

            // update user properties
            user.FirstName = userData.FirstName;
            user.LastName = userData.LastName;
            user.UserName = userData.UserName;

            // update password if it was entered
            if (!string.IsNullOrWhiteSpace(password))
            {
                CreatePasswordHash(password, out var passwordHash, out var passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
        
        public async void DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return;
            
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
        
        public async Task<bool> UserNameInUseAsync(string userName)
        {
            return await _context.Users.AnyAsync(u => u.UserName == userName);
        }
        
        public async Task<bool> EmailInUseAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }
        
        // private helper methods
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) 
                throw new ArgumentNullException(nameof(password));
            
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Value cannot be empty or whitespace only string.", nameof(password));

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null)
                throw new ArgumentNullException(nameof(password));
            
            if (string.IsNullOrWhiteSpace(password)) 
                throw new ArgumentException("Value cannot be empty or whitespace only string.", nameof(password));
            
            if (storedHash.Length != 64)
                throw new ArgumentException("Invalid length of password hash (64 bytes expected).", nameof(password));
            
            if (storedSalt.Length != 128)
                throw new ArgumentException("Invalid length of password salt (128 bytes expected).", nameof(password));

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) 
                        return false;
                }
            }

            return true;
        }
    }
}