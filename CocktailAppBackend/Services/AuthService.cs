using CocktailApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace CocktailAppBackend.Services
{
    public interface IAuthService
    {
        Task AddAuthAsync(string username, string password, string salt, string email, bool isAdmin);
        Task UpdateAuthAsync(int id, string newUsername, string newPassword, string newEmail, bool newIsAdmin);
        Task DeleteAuthAsync(int id);
        Task<string> GetAllAuthsAsync();
        Task<AAuth> GetAuthAsync(int id);
    }
    public class AuthService : IAuthService
    {
        private readonly CocktailAppDBContext _dbContext;

        public AuthService(CocktailAppDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAuthAsync(string username, string password, string salt, string email, bool isAdmin)
        {
            var auth = new Auth {
                Username = username,
                PasswordHash = password,
                EMail = email,
                IsAdmin = isAdmin,
                PasswordSalt = salt
            };
            _dbContext.Auths.Add(auth);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAuthAsync(int id, string newUsername, string newPassword, string newEmail, bool newIsAdmin)
        {
            var auth = await _dbContext.Auths.FindAsync(id);
            if (auth == null)
            {
                throw new Exception($"Auth with ID {id} wasn't found!");
            }
            auth.Username = newUsername;
            auth.PasswordHash = newPassword;
            auth.EMail = newEmail;
            auth.IsAdmin = newIsAdmin;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAuthAsync(int id)
        {
            var auth = await _dbContext.Auths.FindAsync(id);
            if (auth == null)
            {
                throw new Exception($"Auth with ID {id} wasn't found!");
            }
            _dbContext.Auths.Remove(auth);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<string> GetAllAuthsAsync()
        {
            var allAuths = await _dbContext.Auths.ToListAsync();
            var result = new List<AAuthRequestModel>();

            foreach (var auth in allAuths)
            {
                var aAuth = new AAuthRequestModel
                {
                    Username = auth.Username,
                    Password = auth.PasswordHash,
                    Salt = auth.PasswordSalt,
                    EMail = auth.EMail,
                    IsAdmin = auth.IsAdmin
                };

                result.Add(aAuth);
            }

            return JsonSerializer.Serialize(result);
        }

        public async Task<AAuth> GetAuthAsync(int id)
        {
            var auth = await _dbContext.Auths.FindAsync(id);
            if (auth == null)
            {
                throw new Exception($"Auth with ID {id} wasn't found!");
            }
            var aAuth = new AAuth
            {
                Id = auth.Id,
                Username = auth.Username,
                PasswordHash = auth.PasswordHash,
                PasswordSalt = auth.PasswordSalt,
                EMail = auth.EMail,
                IsAdmin = auth.IsAdmin,
                Ratings = new List<int>(),
                OrderList = new List<int>(),
                Favourites = new List<int>(),
            };

            if (auth.Ratings != null && auth.Ratings.Any())
            {
                foreach (var rating in auth.Ratings)
                {
                    aAuth.Ratings.Add(rating.Id);
                }
            }

            if (auth.Orders != null && auth.Orders.Any())
            {
                foreach (var order in auth.Orders)
                {
                    aAuth.OrderList.Add(order.Id);
                }
            }

            if (auth.Favourites != null && auth.Favourites.Any())
            {
                foreach (var favourite in auth.Favourites)
                {
                    aAuth.Favourites.Add(favourite.Id);
                }
            }

            return aAuth;
        }

    }
}
