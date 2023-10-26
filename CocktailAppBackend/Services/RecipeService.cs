using System.Threading.Tasks;
using CocktailApp.Models;
using CocktailAppBackend;
using Microsoft.EntityFrameworkCore;

namespace CocktailAppBackend.Services
{
    public interface IRecipeService
    {
        Task AddRecipeAsync(string username, string password, string email, bool isAdmin);
        Task UpdateRecipeAsync(int id, string newUsername, string newPassword, string newEmail, bool newIsAdmin);
        Task DeleteRecipeAsync(int id);
        Task<List<AAuth>> GetAllRecipesAsync();
        Task<AAuth> GetRecipeAsync(int id);
    }
    public class RecipeService : IRecipeService
    {
        private readonly CocktailAppDBContext _dbContext;

        public RecipeService(CocktailAppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddRecipeAsync(string name, List<Tuple<Ingredient, float>> ingredientsWithAmount, List<Tag> tags)
        {
            var recipe = new Recipe 
            { 
                Name = name, 
                Ingredients = new List<Ingredient>(), 
                Tags = tags 
            };
            
            foreach (var tuple in ingredientsWithAmount)
            {
                var recipeDetail = new RecipeDetail
                {
                    Ingredient = tuple.Item1,
                    AmountInOz = tuple.Item2
                };
                recipe.RecipeDetails.Add(recipeDetail);
            }
            _dbContext.Recipes.Add(recipe);
            await _dbContext.SaveChangesAsync();
            await _dbContext.UpdateRecipeValues();
        }

        public async Task<Recipe> UpdateRecipeAsync(int id, string newName, List<Tuple<Ingredient, float>> newIngredientsWithAmount, List<Tag> newTags)
        {
            var recipe = await _dbContext.Recipes.FindAsync(id);
            if (recipe != null)
            {
                recipe.Name = newName;
                recipe.RecipeDetails.Clear();
                foreach (var tuple in newIngredientsWithAmount)
                {
                    var recipeDetail = new RecipeDetail
                    {
                        Recipe = recipe,
                        Ingredient = tuple.Item1,
                        AmountInOz = tuple.Item2
                    };
                    recipe.RecipeDetails.Add(recipeDetail);
                }
                recipe.Tags.Clear();
                recipe.Tags = newTags;
                await _dbContext.SaveChangesAsync();
            }
            await _dbContext.UpdateRecipeValues();
            return recipe;
        }

        public async Task DeleteRecipeAsync(int id)
        {
            var recipe = await _dbContext.Recipes.FindAsync(id);
            if (recipe != null)
            {
                _dbContext.Recipes.Remove(recipe);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<Recipe?> GetRecipeAsync(int id)
        {
            return await _dbContext.Recipes
                .Include(r => r.Tags)
                .Include(r => r.RecipeDetails)
                .ThenInclude(rd => rd.Ingredient)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<List<Recipe>> GetAllRecipesAsync()
        {
            return await _dbContext.Recipes
                .Include(r => r.Tags)
                .Include(r => r.RecipeDetails)
                .ThenInclude(rd => rd.Ingredient)
                .ToListAsync();
        }

    }
}
