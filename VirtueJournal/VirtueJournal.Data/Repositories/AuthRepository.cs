using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using VirtueJournal.Data.Entities;

namespace VirtueJournal.Data.Repositories
{
    // FROM: https://jasonwatmore.com/post/2018/06/26/aspnet-core-21-simple-api-for-authentication-registration-and-user-management
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;

        public AuthRepository(DataContext context)
        {
            _context = context;
        }
        
        public async Task<User> AuthenticateAsync(string userName, string email, string password)
        {
            var user = !string.IsNullOrWhiteSpace(userName)
                ? await _context.Users.SingleOrDefaultAsync(u => u.UserName == userName)
                : await _context.Users.SingleOrDefaultAsync(u => u.Email == email);

            // check if user exists
            if (user == null)
                return null;

            // check if password is correct
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            // authentication successful
            return user;
        }
        
        public async Task<bool> UserNameInUseAsync(string userName)
        {
            return await _context.Users.AnyAsync(u => u.UserName == userName);
        }
        
        public async Task<bool> EmailInUseAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }
        
        public Task<User> GetByIdAsync(int id)
        {
            return _context.Users.FindAsync(id);
        }
        
        public async Task<User> RegisterAsync(User user, string password)
        {
            CreatePasswordHash(password, out var passwordHash, out var passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }
        
        public void UpdatePassword(User user, string password)
        {
            CreatePasswordHash(password, out var passwordHash, out var passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
        }
        
        public async Task DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return;
            
            _context.Users.Remove(user);
        }
        
        public string GenerateToken(int userId, string secret)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.Name, userId.ToString())
                }),
                Expires = DateTime.UtcNow.AddSeconds(10),
                // Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
        
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
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

        private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
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