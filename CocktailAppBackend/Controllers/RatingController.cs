using CocktailAppBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace CocktailAppBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly IRatingService _ratingService;

        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }
        [HttpPost]
        public async Task<IActionResult> AddRating(int grade, int ratedById, int ratedRecipeId, string? comment)
        {
            await _ratingService.AddRatingAsync(grade, ratedById, ratedRecipeId, comment);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRating(int id, int grade, string? comment)
        {
            await _ratingService.UpdateRatingAsync(id, grade, comment);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRating(int id)
        {
            await _ratingService.DeleteRatingAsync(id);

            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRating(int id)
        {
            var rating = await _ratingService.GetRatingAsync(id);
            if (rating == null)
            {
                return NotFound();
            }
            return Ok(rating);
        }

        [HttpGet("recipe/{id}")]
        public async Task<IActionResult> GetRatingsOfRecipe(int id)
        {
            var ratings = await _ratingService.GetAllRatingsOfRecipeAsync(id);
            if (ratings == null)
            {
                return NotFound();
            }
            return Ok(ratings);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRatings()
        {
            var ratings = await _ratingService.GetAllRatingsAsync();
            return Ok(ratings);
        }
    }
}
