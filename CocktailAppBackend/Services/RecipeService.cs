using System.Diagnostics;
using System.Threading.Tasks;
using CocktailApp.Models;
using CocktailAppBackend;
using Microsoft.EntityFrameworkCore;

namespace CocktailAppBackend.Services
{
    public interface IRecipeService
    {
        Task AddRecipeBaseAsync(string name, string? directions, string? imgPath, string? source);
        Task AddRecipeDetailAsync(int id, int ingredientId, float amountInOz);
        Task AddRecipeTagAsync(int id, int tagId);
        Task AddRecipeFavouriteAsync(int id, int authId);
        Task UpdateRecipeBaseAsync(int id, string name, string? directions, string? imgPath, string? source);
        Task UpdateRecipeDetailAsync(int id, int ingredientId, float amountInOz);
        Task UpdateRecipeTagsAsync(int id, List<int> tagIds);
        Task UpdateRecipeRatingAsync(int id, int ratingId, int grade, string? comment);
        Task DeleteRecipeBaseAsync(int id);
        Task DeleteRecipeDetailAsync(int id, int ingredientId);
        Task DeleteRecipeTagAsync(int id, int tagId);
        Task DeleteRecipeRatingAsync(int id, int authId);
        Task DeleteRecipeFavouriteAsync(int favouriteId);
        Task<List<Tuple<int, float>>> GetRecipeDetailsAsync(int id);
        Task<List<ARecipe>> GetAllRecipesAsync();
        Task<List<ARecipe>> GetAllRecipesWithTag(int tagId);
        Task<List<ARecipe>> GetAllFavouritesByUser(int authId);
        Task<ARecipe> GetRecipeBaseAsync(int id);
    }
    public class RecipeService : IRecipeService
    {
        private readonly CocktailAppDBContext _dbContext;

        public RecipeService(CocktailAppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddRecipeBaseAsync(string name, string? directions, string? imgPath, string? source)
        {
            var baseRecipe = new Recipe
            {
                Name = name,
                Directions = directions,
                ImgPath = imgPath,
                Source = source,
                KcalInTotal = 0,
                IsAvailable = false
            };
            _dbContext.Recipes.Add(baseRecipe);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddRecipeDetailAsync(int id, int ingredientId, float amountInOz)
        {
            var recipe = await _dbContext.Recipes.FindAsync(id);
            if (recipe == null)
            {
                throw new Exception($"Recipe with ID {id} wasn't found!");
            }
            var ingredient = await _dbContext.Ingredients.FindAsync(ingredientId);
            if (ingredient == null)
            {
                throw new Exception($"Ingredient with ID {ingredientId} wasn't found!");
            }

            var recipeDetail = new RecipeDetail
            {
                IngredientId = ingredientId,
                RecipeId = id,
                AmountInOz = amountInOz
            };

            _dbContext.RecipeDetails.Add(recipeDetail);

            await _dbContext.UpdateRecipeValues();
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddRecipeTagAsync(int id, int tagId)
        {
            var recipe = await _dbContext.Recipes.FindAsync(id);
            if (recipe == null)
            {
                throw new Exception($"Recipe with ID {id} wasn't found!");
            }
            var tag = await _dbContext.Tags.FindAsync(tagId);
            if (tag == null)
            {
                throw new Exception($"Tag with ID {tagId} wasn't found!");
            }

            var recipeTag = new RecipeTag
            {
                RecipeId = id,
                TagId = tagId,
            };

            _dbContext.RecipeTags.Add(recipeTag);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddRecipeFavouriteAsync(int id, int authId)
        {
            var ratedRecipe = await _dbContext.Recipes.FindAsync(id);
            if (ratedRecipe == null)
            {
                throw new Exception($"Recipe with ID {id} wasn't found!");
            }

            var favouritedBy = await _dbContext.Auths.FindAsync(authId);
            if (favouritedBy == null)
            {
                throw new Exception($"Auth with ID {authId} wasn't found!");
            }

            var favourite = new Favourite
            {
                RecipeId = id,
                AuthId = authId
            };
            _dbContext.Favourites.Add(favourite);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateRecipeBaseAsync(int id, string name, string? directions, string? imgPath, string? source)
        {
            var recipe = await _dbContext.Recipes.FindAsync(id);
            if (recipe == null)
            {
                throw new Exception($"Recipe with ID {id} wasn't found!");
            }
            recipe.Name = name;
            recipe.Directions = directions;
            recipe.ImgPath = imgPath;
            recipe.Source = source;

            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateRecipeDetailAsync(int id, int ingredientId, float amountInOz)
        {
            if (await _dbContext.Recipes.FindAsync(id) == null)
            {
                throw new Exception($"Recipe with ID {id} wasn't found!");
            }
            if (await _dbContext.Ingredients.FindAsync(ingredientId) == null)
            {
                throw new Exception($"Ingredient with ID {ingredientId} wasn't found!");
            }

            var recipeDetail = await _dbContext.RecipeDetails
                .Where(rd => rd.Ingredient.Id == ingredientId)
                .Where(rd => rd.Recipe.Id == id)
                .Include(o => o.IngredientId)
                .Include(o => o.RecipeId)
                .FirstOrDefaultAsync();

            if (recipeDetail == null)
            {
                throw new Exception($"RecipeDetail with RecipeId {id} and IngredientId {ingredientId} wasn't found!");
            }
            recipeDetail.AmountInOz = amountInOz;

            await _dbContext.SaveChangesAsync();
            await _dbContext.UpdateRecipeValues();
        }
        
        public async Task UpdateRecipeTagsAsync(int id, List<int> tagIds)
        {
            if (await _dbContext.Recipes.FindAsync(id) == null)
            {
                throw new Exception($"Recipe with ID {id} wasn't found!");
            }
            foreach ( int tagId in tagIds )
            {
                if(await _dbContext.Tags.FindAsync(tagId) == null)
                {
                    throw new Exception($"Tag with ID {tagId} wasn't found!");
                }
            }
            var oldRecipeTags = await _dbContext.RecipeTags
                .Where(rt => rt.RecipeId == id)
                .ToListAsync();

            foreach ( RecipeTag oldRecipeTag in oldRecipeTags )
            {
                _dbContext.RecipeTags.Remove(oldRecipeTag);
            }

            foreach (int tagId in tagIds)
            {
                var newRecipeTag = new RecipeTag
                {
                    RecipeId = id,
                    TagId = tagId
                };
                _dbContext.RecipeTags.Add(newRecipeTag);
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateRecipeRatingAsync(int id, int ratingId, int grade, string? comment)
        {
            if (await _dbContext.Recipes.FindAsync(id) == null)
            {
                throw new Exception($"Recipe with ID {id} wasn't found!");
            }

            var rating = await _dbContext.Ratings
                .Where(r => r.RatedRecipe.Id == id)
                .FirstOrDefaultAsync(r => r.Id == ratingId);

            if (rating == null)
            {
                throw new Exception($"Rating with RatingId {ratingId} wasn't found!");
            }
            rating.Grade = grade;
            rating.Comment = comment;

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteRecipeBaseAsync(int id)
        {

            var recipe = await _dbContext.Recipes.FindAsync(id);
            if (recipe == null)
            {
                throw new Exception($"Recipe with ID {id} wasn't found!");
            }

            _dbContext.Recipes.Remove(recipe);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteRecipeDetailAsync(int id, int ingredientId)
        {
            if (await _dbContext.Recipes.FindAsync(id) == null)
            {
                throw new Exception($"Recipe with ID {id} wasn't found!");
            }
            if (await _dbContext.Ingredients.FindAsync(ingredientId) == null)
            {
                throw new Exception($"Ingredient with ID {ingredientId} wasn't found!");
            }

            var recipeDetail = await _dbContext.RecipeDetails
                .Where(rd => rd.Ingredient.Id == ingredientId)
                .Where(rd => rd.Recipe.Id == id)
                .Include(o => o.IngredientId)
                .Include(o => o.RecipeId)
                .FirstOrDefaultAsync();

            if (recipeDetail == null)
            {
                throw new Exception($"RecipeDetail with RecipeId {id} and IngredientId {ingredientId} wasn't found!");
            }

            _dbContext.RecipeDetails.Remove(recipeDetail);
            await _dbContext.SaveChangesAsync();
            await _dbContext.UpdateRecipeValues();
        }

        public async Task DeleteRecipeTagAsync(int id, int tagId)
        {
            var recipe = await _dbContext.Recipes.FindAsync(id);
            if (recipe == null)
            {
                throw new Exception($"Recipe with ID {id} wasn't found!");
            }

            var tag = await _dbContext.Tags.FindAsync(tagId);
            if (tag == null)
            {
                throw new Exception($"Tag with ID {tagId} wasn't found!");
            }

            var recipeTag = await _dbContext.RecipeTags
                .FirstOrDefaultAsync(rt => rt.RecipeId == id && rt.TagId == tagId);

            if (recipeTag == null)
            {
                throw new Exception($"RecipeTag with Recipe ID {id} and Tag ID {tagId} wasn't found!");
            }

            _dbContext.RecipeTags.Remove(recipeTag);
            await _dbContext.SaveChangesAsync();
        }


        public async Task DeleteRecipeRatingAsync(int id, int authId)
        {
            var recipe = await _dbContext.Recipes.FindAsync(id);
            if (recipe == null)
            {
                throw new Exception($"Recipe with ID {id} wasn't found!");
            }

            var auth = await _dbContext.Tags.FindAsync(authId);
            if (auth == null)
            {
                throw new Exception($"Tag with ID {authId} wasn't found!");
            }

            var rating = await _dbContext.Ratings
                .FirstOrDefaultAsync(r => r.RecipeId == id && r.RatedBy.Id == authId);

            if (rating == null)
            {
                throw new Exception($"Rating with Recipe ID {id} and Auth ID {authId} wasn't found!");
            }

            _dbContext.Ratings.Remove(rating);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteRecipeFavouriteAsync(int favouriteId)
        {

        }

        public async Task<List<Tuple<int, float>>> GetRecipeDetailsAsync(int id)
        {
            float f = 0.6F;
            Tuple<int, float> t = new Tuple<int, float>(1,f);
            List<Tuple<int, float>> l = new List<Tuple<int, float>>();
            l.Add(t);
            return l;
        }

        public async Task<List<ARecipe>> GetAllRecipesAsync()
        {
            var allRecipes = await _dbContext.Recipes.ToListAsync();
            var result = new List<ARecipe>();

            foreach (var recipe in allRecipes)
            {
                var aRecipe = new ARecipe
                {
                    Id = recipe.Id,
                    Name = recipe.Name,
                    Directions = recipe.Directions,
                    ImgPath = recipe.ImgPath,
                    Source = recipe.Source,
                    KcalInTotal = recipe.KcalInTotal,
                    IsAvailable = recipe.IsAvailable,
                    Orders = new List<int>(),
                    RecipeDetails = new List<int>(),
                    Ratings = new List<int>(),
                    FavouritedBy = new List<int>(),
                    Tags = new List<int>(),
                };

                if (recipe.Orders != null && recipe.Orders.Any())
                {
                    foreach (var order in recipe.Orders)
                    {
                        aRecipe.Orders.Add(order.Id);
                    }
                }

                if (recipe.RecipeDetails != null && recipe.RecipeDetails.Any())
                {
                    foreach (var recipeDetail in recipe.RecipeDetails)
                    {
                        aRecipe.RecipeDetails.Add(recipeDetail.Id);
                    }
                }

                if (recipe.Ratings != null && recipe.Ratings.Any())
                {
                    foreach (var rating in recipe.Ratings)
                    {
                        aRecipe.Ratings.Add(rating.Id);
                    }
                }

                if (recipe.FavouritedBy != null && recipe.FavouritedBy.Any())
                {
                    foreach (var auth in recipe.FavouritedBy)
                    {
                        aRecipe.FavouritedBy.Add(auth.Id);
                    }
                }

                if (recipe.Tags != null && recipe.Tags.Any())
                {
                    foreach (var tag in recipe.Tags)
                    {
                        aRecipe.Tags.Add(tag.Id);
                    }
                }

                result.Add(aRecipe);
            }

            return result;
        }

        public async Task<List<ARecipe>> GetAllRecipesWithTag(int tagId)
        {
            var allRecipes = await _dbContext.Recipes
                .Where(recipe => recipe.Tags.Any(tag => tag.Id == tagId))
                .ToListAsync();
            var result = new List<ARecipe>();

            foreach (var recipe in allRecipes)
            {
                var aRecipe = new ARecipe
                {
                    Id = recipe.Id,
                    Name = recipe.Name,
                    Directions = recipe.Directions,
                    ImgPath = recipe.ImgPath,
                    Source = recipe.Source,
                    KcalInTotal = recipe.KcalInTotal,
                    IsAvailable = recipe.IsAvailable,
                    Orders = new List<int>(),
                    RecipeDetails = new List<int>(),
                    Ratings = new List<int>(),
                    FavouritedBy = new List<int>(),
                    Tags = new List<int>(),
                };

                if (recipe.Orders != null && recipe.Orders.Any())
                {
                    foreach (var order in recipe.Orders)
                    {
                        aRecipe.Orders.Add(order.Id);
                    }
                }

                if (recipe.RecipeDetails != null && recipe.RecipeDetails.Any())
                {
                    foreach (var recipeDetail in recipe.RecipeDetails)
                    {
                        aRecipe.RecipeDetails.Add(recipeDetail.Id);
                    }
                }

                if (recipe.Ratings != null && recipe.Ratings.Any())
                {
                    foreach (var rating in recipe.Ratings)
                    {
                        aRecipe.Ratings.Add(rating.Id);
                    }
                }

                if (recipe.FavouritedBy != null && recipe.FavouritedBy.Any())
                {
                    foreach (var auth in recipe.FavouritedBy)
                    {
                        aRecipe.FavouritedBy.Add(auth.Id);
                    }
                }

                if (recipe.Tags != null && recipe.Tags.Any())
                {
                    foreach (var tag in recipe.Tags)
                    {
                        aRecipe.Tags.Add(tag.Id);
                    }
                }

                result.Add(aRecipe);
            }

            return result;
        }

        public async Task<List<ARecipe>> GetAllFavouritesByUser(int authId)
        {
            var allRecipes = await _dbContext.Recipes
                .Where(recipe => recipe.FavouritedByAuths.Any(auth => auth.Id == authId))
                .ToListAsync();
            var result = new List<ARecipe>();

            foreach (var recipe in allRecipes)
            {
                var aRecipe = new ARecipe
                {
                    Id = recipe.Id,
                    Name = recipe.Name,
                    Directions = recipe.Directions,
                    ImgPath = recipe.ImgPath,
                    Source = recipe.Source,
                    KcalInTotal = recipe.KcalInTotal,
                    IsAvailable = recipe.IsAvailable,
                    Orders = new List<int>(),
                    RecipeDetails = new List<int>(),
                    Ratings = new List<int>(),
                    FavouritedBy = new List<int>(),
                    Tags = new List<int>(),
                };

                if (recipe.Orders != null && recipe.Orders.Any())
                {
                    foreach (var order in recipe.Orders)
                    {
                        aRecipe.Orders.Add(order.Id);
                    }
                }

                if (recipe.RecipeDetails != null && recipe.RecipeDetails.Any())
                {
                    foreach (var recipeDetail in recipe.RecipeDetails)
                    {
                        aRecipe.RecipeDetails.Add(recipeDetail.Id);
                    }
                }

                if (recipe.Ratings != null && recipe.Ratings.Any())
                {
                    foreach (var rating in recipe.Ratings)
                    {
                        aRecipe.Ratings.Add(rating.Id);
                    }
                }

                if (recipe.FavouritedBy != null && recipe.FavouritedBy.Any())
                {
                    foreach (var auth in recipe.FavouritedBy)
                    {
                        aRecipe.FavouritedBy.Add(auth.Id);
                    }
                }

                if (recipe.Tags != null && recipe.Tags.Any())
                {
                    foreach (var tag in recipe.Tags)
                    {
                        aRecipe.Tags.Add(tag.Id);
                    }
                }

                result.Add(aRecipe);
            }

            return result;
        }

        public async Task<ARecipe> GetRecipeBaseAsync(int id)
        {
            var recipe = await _dbContext.Recipes.FindAsync(id);
            if (recipe == null)
            {
                throw new Exception($"Recipe with ID {id} wasn't found!");
            }
            var aRecipe = new ARecipe
            {
                Id = recipe.Id,
                Name = recipe.Name,
                Directions = recipe.Directions,
                ImgPath = recipe.ImgPath,
                Source = recipe.Source,
                KcalInTotal = recipe.KcalInTotal,
                IsAvailable = recipe.IsAvailable,
                Orders = new List<int>(),
                RecipeDetails = new List<int>(),
                Ratings = new List<int>(),
                FavouritedBy = new List<int>(),
                Tags = new List<int>(),
            };

            if (recipe.Orders != null && recipe.Orders.Any())
            {
                foreach (var order in recipe.Orders)
                {
                    aRecipe.Orders.Add(order.Id);
                }
            }

            if (recipe.RecipeDetails != null && recipe.RecipeDetails.Any())
            {
                foreach (var recipeDetail in recipe.RecipeDetails)
                {
                    aRecipe.RecipeDetails.Add(recipeDetail.Id);
                }
            }

            if (recipe.Ratings != null && recipe.Ratings.Any())
            {
                foreach (var rating in recipe.Ratings)
                {
                    aRecipe.Ratings.Add(rating.Id);
                }
            }

            if (recipe.FavouritedBy != null && recipe.FavouritedBy.Any())
            {
                foreach (var auth in recipe.FavouritedBy)
                {
                    aRecipe.FavouritedBy.Add(auth.Id);
                }
            }

            if (recipe.Tags != null && recipe.Tags.Any())
            {
                foreach (var tag in recipe.Tags)
                {
                    aRecipe.Tags.Add(tag.Id);
                }
            }

            return aRecipe;
        }
    }
}
