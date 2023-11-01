using CocktailApp.Models;
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
        public async Task<IActionResult> AddAuth([FromBody] AAuthRequestModel authRequest)
        {
            Console.WriteLine(authRequest.Username, authRequest.Password, authRequest.Salt, authRequest.EMail, authRequest.IsAdmin);
            await _authService.AddAuthAsync(authRequest.Username, authRequest.Password, authRequest.Salt, authRequest.EMail, authRequest.IsAdmin);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuth(int id, string username, string password, string eMail, bool isAdmin)
        {
            await _authService.UpdateAuthAsync(id, username, password, eMail, isAdmin);
            return Ok();
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
            var auth = await _authService.GetAuthAsync(id);
            if (auth == null)
            {
                return NotFound();
            }
            return Ok(auth);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAuths()
        {
            var auths = await _authService.GetAllAuthsAsync();
            return Ok(auths);
        }
    }
}
