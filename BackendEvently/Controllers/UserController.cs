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
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public UserController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                EmailAddress = user.EmailAddress,
                Role = user.Role
            };
        }

        [HttpGet("{id}/events")]
        public async Task<ActionResult<IEnumerable<EventDto>>> GetUserEvents(int id)
        {
            var events = await _context.EventParticipants
                .Where(ep => ep.UserId == id)
                .Include(ep => ep.Event)
                .Select(ep => new EventDto
                {
                    Id = ep.Event.Id,
                    Name = ep.Event.Name,
                    StartTime = ep.Event.StartTime,
                    Location = ep.Event.Location
                })
                .ToListAsync();

            return Ok(events);
        }
    }
}
