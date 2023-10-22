using System.Threading.Tasks;
using CocktailApp.Models;
using CocktailAppBackend;
using Microsoft.EntityFrameworkCore;

namespace CocktailAppBackend.Services
{
    public interface IRatingService
    {
        Task<Rating> AddRatingAsync(int grade, int ratedById, int ratedRecipeId, string? comment);
        Task<Rating> UpdateRatingAsync(int id, int grade, string? comment);
        Task DeleteRatingAsync(int id);
        Task<List<Rating>> GetAllRatingsAsync();
        Task<List<Rating>> GetAllRatingsOfRecipeAsync(int ratedRecipeId);
        Task<Rating?> GetRatingAsync(int id);
    }
    public class RatingService : IRatingService
    {
        private readonly CocktailAppDBContext _dbContext;

        public RatingService(CocktailAppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Rating> AddRatingAsync(int grade, int ratedById, int ratedRecipeId, string? comment)
        {
            var rating = new Rating { 
                Grade = grade,
                RatedBy = await _dbContext.Auths.FindAsync(ratedById),
                RatedRecipe = await _dbContext.Recipes.FindAsync(ratedRecipeId),
                Comment = comment
            };
            _dbContext.Ratings.Add(rating);
            await _dbContext.SaveChangesAsync();
            return rating;
        }

        public async Task<Rating> UpdateRatingAsync(int id, int grade, string? comment)
        {
            var rating = await _dbContext.Ratings.FindAsync(id);
            if (rating != null)
            {
                rating.Grade = grade;
                rating.Comment = comment;

                await _dbContext.SaveChangesAsync();
            }
            return rating;
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

        public async Task<List<Rating>> GetAllRatingsAsync()
        {
            return await _dbContext.Ratings.ToListAsync();
        }

        public async Task<List<Rating>> GetAllRatingsOfRecipeAsync(int ratedRecipeId)
        {
            return await _dbContext.Ratings
                .Where(r => r.RatedRecipe.Id == ratedRecipeId)
                .ToListAsync();
        }

        public async Task<Rating?> GetRatingAsync(int id)
        {
            return await _dbContext.Ratings.FindAsync(id); ;
        }
    }
}
