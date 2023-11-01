using System.Threading.Tasks;
using CocktailApp.Models;
using CocktailAppBackend;
using Microsoft.EntityFrameworkCore;

namespace CocktailAppBackend.Services
{
    public interface IRatingService
    {
        Task AddRatingAsync(int grade, int ratedById, int ratedRecipeId, string? comment);
        Task DeleteRatingAsync(int id);
        Task UpdateRatingAsync(int id, int grade, string? comment);
        Task<List<ARating>> GetAllRatingsAsync();
        Task<List<ARating>> GetAllRatingsOfRecipeAsync(int ratedRecipeId);
        Task<ARating?> GetRatingAsync(int id);
    }
    public class RatingService : IRatingService
    {
        private readonly CocktailAppDBContext _dbContext;

        public RatingService(CocktailAppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddRatingAsync(int grade, int ratedById, int ratedRecipeId, string? comment)
        {
            var ratedBy = await _dbContext.Auths.FindAsync(ratedById);
            if (ratedBy == null)
            {
                throw new Exception($"Auth with ID {ratedById} wasn't found!");
            }

            var ratedRecipe = await _dbContext.Recipes.FindAsync(ratedRecipeId);
            if (ratedRecipe == null)
            {
                throw new Exception($"Recipe with ID {ratedRecipeId} wasn't found!");
            }

            var rating = new Rating { 
                Grade = grade,
                RatedBy = ratedBy,
                RatedRecipe = ratedRecipe,
                Comment = comment
            };
            _dbContext.Ratings.Add(rating);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateRatingAsync(int id, int grade, string? comment)
        {
            var rating = await _dbContext.Ratings.FindAsync(id);
            if (rating == null)
            {
                throw new Exception($"Rating with ID {id} wasn't found!");
            }
            rating.Grade = grade;
            rating.Comment = comment;

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteRatingAsync(int id)
        {
            var rating = await _dbContext.Ratings.FindAsync(id);
            if (rating != null)
            {
                _dbContext.Ratings.Remove(rating);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<ARating>> GetAllRatingsAsync()
        {
            return await _dbContext.Ratings
                .Include(o => o.RatedRecipe)
                .Include(o => o.RatedBy)
                .Select(r => new ARating
                {
                    Id = r.Id,
                    Grade = r.Grade,
                    Comment = r.Comment,
                    AuthId = r.RatedBy.Id,
                    RecipeId = r.RatedRecipe.Id
                })
                .ToListAsync();
        }

        public async Task<List<ARating>> GetAllRatingsOfRecipeAsync(int ratedRecipeId)
        {
            if (await _dbContext.Recipes.FindAsync(ratedRecipeId) == null)
            {
                throw new Exception($"Recipe with ID {ratedRecipeId} wasn't found!");
            }

            return await _dbContext.Ratings
                .Where(r => r.RatedRecipe.Id == ratedRecipeId)
                .Include(o => o.RatedRecipe)
                .Include(o => o.RatedBy)
                .Select(r => new ARating
                {
                    Id = r.Id,
                    Grade = r.Grade,
                    Comment = r.Comment,
                    AuthId = r.RatedBy.Id,
                    RecipeId = r.RatedRecipe.Id
                })
                .ToListAsync();
        }

        public async Task<ARating?> GetRatingAsync(int id)
        {
            var rating = await _dbContext.Ratings
                .Include(o => o.RatedRecipe)
                .Include(o => o.RatedBy)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (rating == null)
            {
                throw new Exception($"Rating with ID {id} wasn't found!");
            }

            return new ARating
            {
                Id = rating.Id,
                Comment = rating.Comment,
                Grade = rating.Grade,
                AuthId = rating.RatedBy.Id,
                RecipeId = rating.RatedRecipe.Id
            };
        }
    }
}
