using System.Threading.Tasks;
using CocktailApp.Models;
using CocktailAppBackend;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Extensions.Msal;

namespace CocktailAppBackend.Services
{
    public interface IIngredientService
    {
        Task AddIngredientAsync(string name, float kcal, string? ImgPath, bool inStorage);
        Task DeleteIngredientAsync(int ingredientId);
        Task<Ingredient> UpdateIngredientAsync(int ingredientId, string newName, float newKcal, string? newImgPath, bool newInStorage);
        Task<List<AIngredient>> GetAllIngredientsAsync();
        Task<AIngredient> GetOneIngredientAsync(int ingredientId);
    }
    public class IngredientService : IIngredientService
    {
        private readonly CocktailAppDBContext _dbContext;

        public IngredientService(CocktailAppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddIngredientAsync(string name, float kcal, string? ImgPath, bool inStorage)
        {
            var ingredient = new Ingredient
            {
                Name = name,
                Kcal = kcal,
                InStorage = inStorage,
                ImgPath = ImgPath
            };

            await _dbContext.Ingredients.AddAsync(ingredient);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Ingredient> UpdateIngredientAsync(int ingredientId, string newName, float newKcal, string? newImgPath, bool newInStorage)
        {
            var ingredient = await _dbContext.Ingredients.FindAsync(ingredientId);
            if (ingredient == null)
            {
                throw new Exception($"Ingredient with ID {ingredientId} wasn't found!");
            }
            ingredient.Name = newName;
            ingredient.Kcal = newKcal;
            ingredient.ImgPath = newImgPath;
            ingredient.InStorage = newInStorage;
            await _dbContext.SaveChangesAsync();
            await _dbContext.UpdateRecipeValues();
            return ingredient;
        }

        public async Task DeleteIngredientAsync(int id)
        {
            var ingredient = await _dbContext.Ingredients.FindAsync(id);
            if (ingredient == null)
            {
                throw new Exception($"Ingredient with ID {id} wasn't found!");
            }
            _dbContext.Ingredients.Remove(ingredient);
            await _dbContext.SaveChangesAsync();
            await _dbContext.UpdateRecipeValues();
        }

        public async Task<List<AIngredient>> GetAllIngredientsAsync()
        {
            var allIngredients = await _dbContext.Ingredients.ToListAsync();
        
            var result = new List<AIngredient>();

            foreach (var ingredient in allIngredients)
            {
                var aIngredient = new AIngredient
                {
                    Id = ingredient.Id,
                    Name = ingredient.Name,
                    Kcal = ingredient.Kcal,
                    InStorage = ingredient.InStorage,
                    ImgPath = ingredient.ImgPath,
                    Recipes = new List<int>(),
                };

                if (ingredient.Recipes != null && ingredient.Recipes.Any())
                {
                    foreach (var recipe in ingredient.Recipes)
                    {
                        aIngredient.Recipes.Add(recipe.Id);
                    }
                }

                result.Add(aIngredient);
            }

            return result;
        }

        public async Task<AIngredient> GetOneIngredientAsync(int id)
        {
            var ingredient = await _dbContext.Ingredients
                .Include(o => o.Recipes)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (ingredient == null)
            {
                throw new Exception($"Ingredient with ID {id} wasn't found!");
            }
            var aIngredient =  new AIngredient
            {
                Id = ingredient.Id,
                Name = ingredient.Name,
                Kcal = ingredient.Kcal,
                InStorage = ingredient.InStorage,
                ImgPath = ingredient.ImgPath,
                Recipes = new List<int>(),
            };

            if (ingredient.Recipes != null && ingredient.Recipes.Any())
            {
                foreach (var recipe in ingredient.Recipes)
                {
                    aIngredient.Recipes.Add(recipe.Id);
                }
            }

            return aIngredient;

        }
    }
}