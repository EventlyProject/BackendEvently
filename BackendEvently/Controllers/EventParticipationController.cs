using BackendEvently.Data;
using BackendEvently.Dtos;
using BackendEvently.Model;
using Evently.Shared.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackendEvently.Controllers
{
    [Route("api/participations")]
    [ApiController]
    [Authorize]
    public class EventParticipationController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public EventParticipationController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterForEvent([FromBody] ParticipationDto participationDto)
        {
            // Check if already registered
            var existing = await _context.EventParticipants
                .FirstOrDefaultAsync(ep => ep.EventId == participationDto.EventId &&
                                         ep.UserId == participationDto.UserId);
            if (existing != null)
            {
                return BadRequest("User already registered for this event");
            }

            var participation = new EventParticipant
            {
                UserId = participationDto.UserId,
                EventId = participationDto.EventId,
                RegisteretAt = DateTime.Now
            };

            _context.EventParticipants.Add(participation);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> CancelRegistration([FromBody] ParticipationDto participationDto)
        {
            var participation = await _context.EventParticipants
                .FirstOrDefaultAsync(ep => ep.EventId == participationDto.EventId &&
                                         ep.UserId == participationDto.UserId);
            if (participation == null)
            {
                return NotFound();
            }

            _context.EventParticipants.Remove(participation);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
