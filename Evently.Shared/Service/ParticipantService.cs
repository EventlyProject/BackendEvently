using AutoMapper;
using BackendEvently.Data;
using BackendEvently.Dtos;
using BackendEvently.Model;
using Evently.Shared.Dtos;
using Evently.Shared.Service.InterfaceService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evently.Shared.Service
{
    public class ParticipantService : IParticipantService
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public ParticipantService(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ParticipationDto>RegisterAsync(int userId,int eventId)
        {
            var exists = await _context.EventParticipants
                .AnyAsync(p=>p.UserId==userId&&p.EventId == eventId);
            if (exists) throw new Exception("User already register for this event");

            var participation = new EventPartipaint
            {
                UserId = userId,
                EventId = eventId
            };

            _context.EventParticipants.Add(participation);
            await _context.SaveChangesAsync();

            return _mapper.Map<ParticipationDto>(participation);
        }

        public async Task<IEnumerable<UserDto>>GetParticipantsByEventIdAsync(int eventId)
        {
            var praticipant = await _context.EventParticipants
                .Include(p=>p.User)
                .Where(p=>p.EventId == eventId)
                .Select(p=>p.User!)
                .ToListAsync();
            return _mapper.Map<IEnumerable<UserDto>>(praticipant);
        }

        public async Task<IEnumerable<EventDto>>GetEventsByUserIdAsync(int userId)
        {
            var events = await _context.EventParticipants
                .Include(p=>p.Event)
                .Where(p=>p.UserId == userId)
                .Select(p=>p.Event!)
                .ToListAsync();
            return _mapper.Map<IEnumerable<EventDto>>(events);
        }

        public async Task<bool> RemoveParticipationAsync(int participationId)
        {
            var participation = await _context.EventParticipants.FindAsync(participationId);
            if (participation == null) return false;

            _context.EventParticipants.Remove(participation);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ParticipationDto> GetParticipationByIdAsync(int participationId)
        {
            var participation = _context.EventParticipants
                .Include(p => p.User)
                .Include(p => p.Event)
                .FirstOrDefaultAsync(p => p.Id == participationId);
            if (participation == null)
                return null;
            return _mapper.Map<ParticipationDto>(participation);
        }
    }
}
