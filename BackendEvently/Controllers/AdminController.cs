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

        public AdminController(IUserService userService, IEventService eventService,ICategoryService categoryService,IParticipantService participantService)
        {
            _userService = userService;
            _eventService = eventService;
            _categoryService = categoryService;
            _participantService = participantService;
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        [HttpPut("users/{id}/promote")]
        public async Task<IActionResult>PromoteUserToAdmin(int id)
        {
            var result = await _userService.GetAllAsync();
            return Ok(result);
        }

        [HttpDelete("user/{id}")]
        public async Task<IActionResult>DeleteUser(int id)
        {
            var result = await _userService.DeleteUserAsync(id);
            if (!result) return NotFound("User not found.");
            return Ok("User deleted.");
        }

        [HttpGet("events")]
        public async Task<IActionResult> GetAllEvent()
        {
            var events = await _eventService.GetAllAsync();
            return Ok(events);
        }

        [HttpGet("categoryes")]
        public async Task<IActionResult> HetAllCategoryes()
        {
            var categoryes = await _categoryService.GetAllAsync();
            return Ok(categoryes);
        }

        [HttpDelete("participations/{id}")]
        public async Task<IActionResult> RemoveParticipation(int participationId)
        {
            var result = await _participantService.RemoveParticipationAsync(participationId);
            if (!result) return NotFound("Participation not found");
            return Ok("Participation removed");
        }
    }
}
