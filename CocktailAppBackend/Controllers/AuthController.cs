using CocktailAppBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace CocktailAppBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost]
        public async Task<IActionResult> AddAuth(string username, string password, string eMail, bool isAdmin)
        {
            await _authService.AddAuthAsync(username, password, eMail, isAdmin);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuth(int id, string username, string password, string eMail, bool isAdmin)
        {
            var result = await _authService.UpdateAuthAsync(id, username, password, eMail, isAdmin);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuth(int id)
        {
            await _authService.DeleteAuthAsync(id);

            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthById(int id)
        {
            var order = await _authService.GetAuthAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAuths()
        {
            var auths = await _authService.GetAllAuthsAsync();
            return Ok(auths);
        }
    }
}
