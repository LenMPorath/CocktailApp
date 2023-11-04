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
        private readonly ITokenService _tokenService;

        public AuthController(IAuthService authService, ITokenService tokenService)
        {
            _authService = authService;
            _tokenService = tokenService;
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

        [HttpGet("passwordVerify/{email}/{password}")]
        public async Task<IActionResult> VerifyPassword(string email, string password)
        {
            var tuple = await _authService.VerifyPasswordAsync(email, password);

            if (!tuple.Item2)
            {
                return BadRequest( new { message = "Password is wrong" });
            }

            return Ok(new {
                Token = _tokenService.GenerateToken(tuple.Item1.Id),
                Nutzername = tuple.Item1.Username,
                UserId = tuple.Item1.Id,
                isAdmin = tuple.Item1.IsAdmin    
            });
        }

        [HttpGet("GetSalt/{email}")]
        public async Task<IActionResult> GetSalt(string email)
        {
            string salt = await _authService.GetSaltAsync(email);
            if (salt == null)
            {
                return NotFound(); // oder eine ähnliche Fehlerantwort zurückgeben
            }
            return Ok(salt);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAuths()
        {
            var auths = await _authService.GetAllAuthsAsync();
            return Ok(auths);
        }
    }
}
