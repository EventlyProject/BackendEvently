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

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult>GetById(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if(user==null) return NotFound();
            return Ok(user);
        }

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
                return BadRequest(new {error=ex.Message});
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult>UpdateUser(int id, [FromBody]UpdateUserDto dto)
        {
            var updated = await _userService.UpdateUserAsync(id, dto);
            if (updated == null) return NotFound("User not found.");
            return Ok(updated);
        }
    }
}
