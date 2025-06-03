using Evently.Shared.Service;
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
        private readonly IEventService _eventService;
        // Injects the participant service for participant operations
        public ParticipantController(IParticipantService participantService, IEventService eventService)
        {
            _participantService = participantService;
            _eventService = eventService;
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
        [Authorize] // Any authenticated user
        [HttpDelete("events/{eventId}/participations/{participationId}")]
        public async Task<IActionResult> RemoveParticipationAsOwner(int eventId, int participationId)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized();

            int currentUserId = int.Parse(userIdClaim.Value);

            // Get the event to check ownership
            var eventDto = await _eventService.GetByIdAsync(eventId);
            if (eventDto == null)
                return NotFound("Event not found");

            if (eventDto.UserId != currentUserId)
                return Forbid();

            var result = await _participantService.RemoveParticipationAsync(participationId);
            if (!result) return NotFound("Participation not found");
            return Ok("Participation removed");
        }
    }
}
