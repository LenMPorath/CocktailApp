using CocktailAppBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace CocktailAppBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpPost]
        public async Task<IActionResult> AddTag(string title)
        {
            await _tagService.AddTagAsync(title);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTag(int id, string newTitle)
        {
            await _tagService.UpdateTagAsync(id, newTitle);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTag(int id)
        {
            await _tagService.DeleteTagAsync(id);

            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTag(int id)
        {
            var rating = await _tagService.GetTagAsync(id);
            if (rating == null)
            {
                return NotFound();
            }
            return Ok(rating);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTags()
        {
            var ratings = await _tagService.GetAllTagsAsync();
            return Ok(ratings);
        }
    }
}
