using BackendEvently.Data;
using BackendEvently.Dtos;
using BackendEvently.Model;
using Evently.Shared.Service.InterfaceService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackendEvently.Controllers
{
    [Route("api/admin")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IEventService _eventService;
        private readonly ICategoryService _categoryService;
        private readonly IParticipantService _participantService;
        private readonly IAdminService _adminService;

        // Constructor injects all required services for admin operations
        public AdminController(IUserService userService, IEventService eventService,ICategoryService categoryService,
            IParticipantService participantService, IAdminService adminService)
        {
            _userService = userService;
            _eventService = eventService;
            _categoryService = categoryService;
            _participantService = participantService;
            _adminService = adminService;
        }
        // Get a list of all users
        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }
        // Promote a user to admin role
        [HttpPut("users/{id}/promote")]
        public async Task<IActionResult>PromoteUserToAdmin(int id)
        {
            var result = await _adminService.PromoteToAdminAsync(id);
            if (!result) return NotFound("User not found.");
            return Ok("User promoted to admin.");
        }
        // Delete a user by ID
        [HttpDelete("user/{id}")]
        public async Task<IActionResult>DeleteUser(int id)
        {
            var result = await _userService.DeleteUserAsync(id);
            if (!result) return NotFound("User not found.");
            return Ok("User deleted.");
        }
        // Get a list of all events
        [HttpGet("events")]
        public async Task<IActionResult> GetAllEvent()
        {
            var events = await _eventService.GetAllAsync();
            return Ok(events);
        }
        // Get a list of all categories
        [HttpGet("categories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllAsync();
            return Ok(categories);
        }
        // Remove a participant from an event by participation ID
        [HttpDelete("participations/{id}")]

        public async Task<IActionResult> RemoveParticipation(int id)
        {
            var result = await _participantService.RemoveParticipationAsync(id);
            if (!result) return NotFound("Participation not found");
            return Ok("Participation removed");
        }
    }
}
