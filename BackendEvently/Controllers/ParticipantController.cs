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

        public ParticipantController(IParticipantService participantService)
        {
            _participantService = participantService;
        }

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
                return BadRequest(new { ex.Message });
            }
        }

        [HttpGet("event/{eventId}")]
        public async Task<IActionResult> GetParticipants(int eventId)
        {
            var participants = await _participantService.GetParticipantsByEventIdAsync(eventId);
            return Ok(participants);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserEvents(int userId)
        {
            var events = await _participantService.GetEventsByUserIdAsync(userId);
            return Ok(events);
        }

        [HttpDelete("{paricioationId}")]
        [Authorize]
        public async Task<IActionResult>RemoveParticipation(int paricioationId)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            var roleClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);

            if (userIdClaim == null || roleClaim == null)
                return Unauthorized();

            int userId = int.Parse(userIdClaim.Value);
            string role = roleClaim.Value;

            var Paricipation = await _participantService.GetParticipationByIdAsync(paricioationId);
            if (Paricipation == null)
                return NotFound("Participation not found");

            if(role=="Admin"|| Paricipation.EventId == userId)
            {
                var result = await _participantService.RemoveParticipationAsync(paricioationId);
                if (!result) return NotFound("Participation not found");
                return Ok("Paricipation removied");
            }
            return Forbid();
        }
    }
}
