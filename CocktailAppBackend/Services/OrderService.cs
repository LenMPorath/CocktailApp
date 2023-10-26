using System.Threading.Tasks;
using CocktailApp.Models;
using CocktailAppBackend;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CocktailAppBackend.Services
{
    public interface IOrderService
    {
        Task AddOrderAsync(int recipeId, int authId, DateTime createdAt, int amount, string? note, string status);
        Task DeleteOrderAsync(int id);
        Task UpdateOrderAsync(int id, int recipeId, int amount, string? note, string status);
        Task<List<AOrder>> GetAllOrderAsync();
        Task<AOrder> GetOrderAsync(int id);
    }
    public class OrderService : IOrderService
    {
        private readonly CocktailAppDBContext _dbContext;

        public OrderService(CocktailAppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddOrderAsync(int recipeId, int authId, DateTime createdAt, int amount, string? note, string status)
        {
            var recipe = await _dbContext.Recipes.FindAsync(recipeId);

            if (recipe == null)
            {
                throw new ArgumentException($"Recipe with id {recipeId} not found");
            }

            var auth = await _dbContext.Auths.FindAsync(authId);

            if (auth == null)
            {
                throw new ArgumentException($"Auth with id {authId} not found");
            }
            var order = new Order
            {
                Recipe = recipe,
                CreatedByUser = auth,
                CreatedAt = createdAt,
                Amount = amount,
                Note = note,
                Status = status
            };
            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(int id)
        {
            var order = await _dbContext.Orders.FindAsync(id);
            if (order == null)
            {
                throw new Exception($"Order with ID {id} wasn't found!");
            }
            _dbContext.Orders.Remove(order);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateOrderAsync(int id, int recipeId, int amount, string? note, string status)
        {
            var existingOrder = await _dbContext.Orders
                .Include(o => o.Recipe)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (existingOrder == null)
            {
                throw new ArgumentException($"Order with id {id} not found");
            }

            var existingRecipe = await _dbContext.Recipes.FindAsync(recipeId);

            if (existingRecipe == null)
            {
                throw new ArgumentException($"Recipe with id {recipeId} not found");
            }

            existingOrder.Recipe = existingRecipe;
            existingOrder.Amount = amount;
            existingOrder.Note = note;
            existingOrder.Status = status;

            await _dbContext.SaveChangesAsync();
        }


        public async Task<List<AOrder>> GetAllOrderAsync()
        {
            return await _dbContext.Orders
                .Select(o => new AOrder
                {
                    Id = o.Id,
                    RecipeId = o.Recipe.Id,
                    CreatedByUserId = o.CreatedByUser.Id,
                    CreatedAt = o.CreatedAt,
                    Amount = o.Amount,
                    Note = o.Note,
                    Status = o.Status
                })
                .ToListAsync();
        }

        public async Task<AOrder> GetOrderAsync(int id)
        {
            var order = await _dbContext.Orders
                .Include(o => o.Recipe)
                .Include(o => o.CreatedByUser)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                throw new Exception($"Order with ID {id} wasn't found!");
            }

            return new AOrder
            {
                Id = order.Id,
                RecipeId = order.Recipe.Id,
                CreatedByUserId = order.CreatedByUser.Id,
                CreatedAt = order.CreatedAt,
                Amount = order.Amount,
                Note = order.Note,
                Status = order.Status
            };
        }
    }
}