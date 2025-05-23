using BackendEvently.Dtos;
using BackendEvently.Service;
using Microsoft.AspNetCore.Mvc;

namespace BackendEvently.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IJwtService _jwtService;
        private readonly IEmailService _emailService;

        public AuthenticationController(IJwtService jwtService, IEmailService emailService)
        {
            _jwtService = jwtService;
            _emailService = emailService;
        }
    }
}