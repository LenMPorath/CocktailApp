using System.Threading.Tasks;
using CocktailApp.Models;
using CocktailAppBackend;
using Microsoft.EntityFrameworkCore;

namespace CocktailAppBackend.Services
{
    public class IngredientService
    {
        private readonly CocktailAppDBContext _dbContext;

        public IngredientService(CocktailAppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddIngredientAsync(string name, int kcal)
        {
            var ingredient = new Ingredient
            {
                Name = name,
                Kcal = kcal
            };

            await _dbContext.Ingredients.AddAsync(ingredient);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateIngredientAsync(int ingredientId, string newName, int newKcal)
        {
            var ingredient = await _dbContext.Ingredients.FindAsync(ingredientId);

            if (ingredient != null)
            {
                ingredient.Name = newName;
                ingredient.Kcal = newKcal;
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteIngredientAsync(int ingredientId)
        {
            var ingredient = await _dbContext.Ingredients.FindAsync(ingredientId);

            if (ingredient != null)
            {
                _dbContext.Ingredients.Remove(ingredient);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<Ingredient>> GetAllIngredientsAsync()
        {
            return await _dbContext.Ingredients.ToListAsync();
        }

        public async Task<Ingredient?> GetOneIngredientAsync(int ingredientId)
        {
                return await _dbContext.Ingredients.FindAsync(ingredientId);
        }
    }
}