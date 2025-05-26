using Evently.Shared.Service.InterfaceService;
using Microsoft.AspNetCore.Mvc;

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
    }
}
