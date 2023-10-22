using CocktailAppBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace CocktailAppBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientController : ControllerBase
    {
        private readonly IIngredientService _ingredientService;

        public IngredientController(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }
    

        [HttpPost]
        public async Task<IActionResult> AddIngredient(string name, int kcal)
        {
            await _ingredientService.AddIngredientAsync(name, kcal);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIngredient(int id, string name, int kcal)
        {
            var result = await _ingredientService.UpdateIngredientAsync(id, name, kcal);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngredient(int id)
        {
            await _ingredientService.DeleteIngredientAsync(id);

            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneIngredient(int id)
        {
            var order = await _ingredientService.GetOneIngredientAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllIngredients()
        {
            var auths = await _ingredientService.GetAllIngredientsAsync();
            return Ok(auths);
        }
    }
}
