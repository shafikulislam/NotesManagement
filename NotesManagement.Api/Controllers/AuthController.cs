using Microsoft.AspNetCore.Mvc;
using NotesManagement.Api.DTOs;
using NotesManagement.Api.Services.Interfaces;

namespace NotesManagement.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Register([FromBody] RegisterDto userDto)
        {
            try
            {
                var authenticatedUser = await _authService.RegisterAsync(userDto);
                return Ok(authenticatedUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto userDto)
        {
            try
            {
                var authenticatedUser = await _authService.LoginAsync(userDto);
                return Ok(authenticatedUser);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _authService.LogoutAsync();
            return Ok("Logged out successfully");
        }
    }
}
