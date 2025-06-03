using BackendEvently.Dtos;
using BackendEvently.Service;
using Evently.Shared.Service.InterfaceService;
using Microsoft.AspNetCore.Mvc;

namespace BackendEvently.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IJwtService _jwtService;

        // Injects the JWT service for authentication and token generation
        public AuthenticationController(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }
        // Authenticates a user and returns a JWT token if successful
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            // Try to authenticate the user with the provided credentials
            var user = await _jwtService.AuthenticateAsync(dto);
            if (user == null)
                // Return 401 if authentication fails
                return Unauthorized(new { error = "Invalid credentoals" });
            // Generate a JWT token for the authenticated user
            var token = _jwtService.GenerateToken(user);
            // Return the token and user info
            return Ok(new { token, user });
        }
    }
}