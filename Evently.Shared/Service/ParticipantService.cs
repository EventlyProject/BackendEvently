using AutoMapper;
using BackendEvently.Data;
using BackendEvently.Dtos;
using BackendEvently.Model;
using Evently.Shared.Dtos;
using Evently.Shared.Service.InterfaceService;
using Microsoft.EntityFrameworkCore;
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
                .Include(p=>p.EventId)
                .Where(p=>p.UserId == userId)
                .Select(p=>p.Event!)
                .ToListAsync();
            return _mapper.Map<IEnumerable<EventDto>>(events);
        }
    }
}
