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

        public AuthenticationController(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = await _jwtService.AuthenticateAsync(dto);
            if (user == null)
                return Unauthorized(new { error = "Invalid credentoals" });

            var token = _jwtService.GenerateToken(user);
            return Ok(new { token, user });
        }
    }
}