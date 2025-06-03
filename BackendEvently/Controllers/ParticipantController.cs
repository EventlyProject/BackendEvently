using Evently.Shared.Service.InterfaceService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BackendEvently.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParticipantController :ControllerBase
    {
        private readonly IParticipantService _participantService;
        // Injects the participant service for participant operations
        public ParticipantController(IParticipantService participantService)
        {
            _participantService = participantService;
        }
        // Register a user for an event
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromQuery]int userId, [FromQuery] int eventId)
        {
            try
            {
                var result = await _participantService.RegisterAsync(userId, eventId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Return 400 if registration fails (e.g., already registered)
                return BadRequest(new { ex.Message });
            }
        }
        // Get all participants for a specific event
        [HttpGet("event/{eventId}")]
        public async Task<IActionResult> GetParticipants(int eventId)
        {
            var participants = await _participantService.GetParticipantsByEventIdAsync(eventId);
            return Ok(participants);
        }
        // Get all events a specific user is participating in
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserEvents(int userId)
        {
            var events = await _participantService.GetEventsByUserIdAsync(userId);
            return Ok(events);
        }
        // Remove a participant from an event (only event owner or admin can do this)
        [HttpDelete("{paricioationId}")]
        [Authorize]
        public async Task<IActionResult>RemoveParticipation(int paricioationId)
        {
            // Extract user ID and role from JWT claims
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            var roleClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);

            if (userIdClaim == null || roleClaim == null)
                return Unauthorized();

            int userId = int.Parse(userIdClaim.Value);
            string role = roleClaim.Value;
            // Fetch the participation to check event ownership
            var Paricipation = await _participantService.GetParticipationByIdAsync(paricioationId);
            if (Paricipation == null)
                return NotFound("Participation not found");
            // Allow removal if user is admin or event owner
            if (role=="Admin"|| Paricipation.EventId == userId)
            {
                var result = await _participantService.RemoveParticipationAsync(paricioationId);
                if (!result) return NotFound("Participation not found");
                return Ok("Paricipation removied");
            }
            // Forbid if not authorized
            return Forbid();
        }
    }
}
