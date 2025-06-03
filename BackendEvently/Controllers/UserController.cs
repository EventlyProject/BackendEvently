using AutoMapper.Configuration.Annotations;
using BackendEvently.Data;
using BackendEvently.Dtos;
using BackendEvently.Model;
using Evently.Shared.Dtos;
using Evently.Shared.Service.InterfaceService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackendEvently.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        // Injects the user service for user operations
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        // Get a list of all users
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }
        // Get a single user by their ID
        [HttpGet("{id}")]
        public async Task<IActionResult>GetById(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if(user==null) return NotFound();
            return Ok(user);
        }
        // Register a new user
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            try
            {
                var created = await _userService.RegisterAsync(dto);
                return Ok(created);
            }
            catch (Exception ex)
            {
                // Return 400 if registration fails (e.g., duplicate email)
                return BadRequest(new {error=ex.Message});
            }
        }
        // Update an existing user by their ID
        [HttpPut("{id}")]
        public async Task<IActionResult>UpdateUser(int id, [FromBody]UpdateUserDto dto)
        {
            var updated = await _userService.UpdateUserAsync(id, dto);
            if (updated == null) return NotFound("User not found.");
            return Ok(updated);
        }
    }
}
