using CocktailAppBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace CocktailAppBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeService _recipeService;

        public RecipeController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }
        [HttpPost]
        public async Task<IActionResult> AddRecipeBase(string name, string? directions, string? imgPath, string? source)
        {
            await _recipeService.AddRecipeBaseAsync(name, directions, imgPath, source);
            return Ok();
        }

        [HttpPost("recipedetail/{id}")]
        public async Task<IActionResult> AddRecipeDetail(int id, int ingredientId, float amountInOz)
        {
            await _recipeService.AddRecipeDetailAsync(id, ingredientId, amountInOz);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRecipeBase(int id, string name, string? directions, string? imgPath, string? source)
        {
            await _recipeService.UpdateRecipeBaseAsync(id, name, directions, imgPath, source);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipeBase(int id)
        {
            await _recipeService.DeleteRecipeBaseAsync(id);

            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecipeBaseById(int id)
        {
            var recipe = await _recipeService.GetRecipeBaseAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }
            return Ok(recipe);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRecipes()
        {
            var recipes = await _recipeService.GetAllRecipesAsync();
            return Ok(recipes);
        }
    }
}
