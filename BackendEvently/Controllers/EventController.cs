using BackendEvently.Data;
using BackendEvently.Dtos;
using BackendEvently.Model;
using Evently.Shared.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackendEvently.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public EventController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventDto>>> GetEvents()
        {
            return await _context.Events
                .Select(e => new EventDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    LogoUrl = e.LogoUrl,
                    StartTime = e.StartTime,
                    Details = e.Details,
                    Location = e.Location,
                    MaxParticipants = e.MaxParticipants,
                    Price = e.Price,
                    AccessRequirements = e.AccessRequirements,
                    CategoryId = e.CategoryId
                })
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EventDto>> GetEvent(int id)
        {
            var @event = await _context.Events.FindAsync(id);

            if (@event == null)
            {
                return NotFound();
            }

            return new EventDto
            {
                Id = @event.Id,
                Name = @event.Name,
                LogoUrl = @event.LogoUrl,
                StartTime = @event.StartTime,
                Details = @event.Details,
                Location = @event.Location,
                MaxParticipants = @event.MaxParticipants,
                Price = @event.Price,
                AccessRequirements = @event.AccessRequirements,
                CategoryId = @event.CategoryId
            };
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Organizer")]
        public async Task<ActionResult<EventDto>> CreateEvent([FromBody] CreateEventDto eventDto)
        {
            var @event = new Event
            {
                Name = eventDto.Name,
                LogoUrl = eventDto.LogoUrl,
                StartTime = eventDto.StartTime,
                Details = eventDto.Details,
                Location = eventDto.Location,
                MaxParticipants = eventDto.MaxParticipants,
                Price = eventDto.Price,
                AccessRequirements = eventDto.AccessRequirements,
                CategoryId = eventDto.CategoryId
            };

            _context.Events.Add(@event);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEvent), new { id = @event.Id }, eventDto);
        }
    }
}
