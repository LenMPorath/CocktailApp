using CocktailApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CocktailAppBackend.Services
{
    public interface IAuthService
    {
        Task<Auth> AddAuthAsync(string username, string password, string email, bool isAdmin);
        Task<Auth> UpdateAuthAsync(int id, string newUsername, string newPassword, string newEmail, bool newIsAdmin);
        Task DeleteAuthAsync(int id);
        Task<List<Auth>> GetAllAuthsAsync();
        Task<Auth?> GetAuthAsync(int id);
    }
    public class AuthService : IAuthService
    {
        private readonly CocktailAppDBContext _dbContext;

        public AuthService(CocktailAppDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Auth> AddAuthAsync(string username, string password, string email, bool isAdmin)
        {
            var auth = new Auth { 
                Username = username, 
                Password = password, 
                EMail = email, 
                IsAdmin = isAdmin,
                
            };
            _dbContext.Auths.Add(auth);
            await _dbContext.SaveChangesAsync();
            return auth;
        }

        public async Task<Auth> UpdateAuthAsync(int id, string newUsername, string newPassword, string newEmail, bool newIsAdmin)
        {
            var auth = await _dbContext.Auths.FindAsync(id);
            if (auth != null)
            {
                auth.Username = newUsername;
                auth.Password = newPassword;
                auth.EMail = newEmail;
                auth.IsAdmin = newIsAdmin;
                await _dbContext.SaveChangesAsync();
            }
            return auth;
        }

        public async Task DeleteAuthAsync(int id)
        {
            var auth = await _dbContext.Auths.FindAsync(id);
            if (auth != null)
            {
                _dbContext.Auths.Remove(auth);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<Auth>> GetAllAuthsAsync()
        {
            return await _dbContext.Auths.ToListAsync();
        }

        public async Task<Auth?> GetAuthAsync(int id)
        {
            return await _dbContext.Auths.FindAsync(id);
        }

    }
}
