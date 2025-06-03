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

        // Constructor to inject the database context and AutoMapper
        public ParticipantService(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Registers a user for an event if not already registered
        public async Task<ParticipationDto> RegisterAsync(int userId, int eventId)
        {
            // Check if the user is already registered for the event
            var exists = await _context.EventParticipants
                .AnyAsync(p => p.UserId == userId && p.EventId == eventId);
            if (exists) throw new Exception("User already register for this event");

            // Create a new participation record
            var participation = new EventPartipaint
            {
                UserId = userId,
                EventId = eventId
            };

            // Add and save the new participation
            _context.EventParticipants.Add(participation);
            await _context.SaveChangesAsync();

            // Map and return the participation DTO
            return _mapper.Map<ParticipationDto>(participation);
        }

        // Retrieves all participants for a specific event
        public async Task<IEnumerable<UserDto>> GetParticipantsByEventIdAsync(int eventId)
        {
            // Query participants and include user details
            var praticipant = await _context.EventParticipants
                .Include(p => p.User)
                .Where(p => p.EventId == eventId)
                .Select(p => p.User!)
                .ToListAsync();
            // Map and return the list of user DTOs
            return _mapper.Map<IEnumerable<UserDto>>(praticipant);
        }

        // Retrieves all events a specific user is participating in
        public async Task<IEnumerable<EventDto>> GetEventsByUserIdAsync(int userId)
        {
            // Query events and include event details
            var events = await _context.EventParticipants
                .Include(p => p.Event)
                .Where(p => p.UserId == userId)
                .Select(p => p.Event!)
                .ToListAsync();
            // Map and return the list of event DTOs
            return _mapper.Map<IEnumerable<EventDto>>(events);
        }

        // Removes a participation record by its ID
        public async Task<bool> RemoveParticipationAsync(int participationId)
        {
            // Find the participation record
            var participation = await _context.EventParticipants.FindAsync(participationId);
            if (participation == null) return false;

            // Remove and save changes
            _context.EventParticipants.Remove(participation);
            await _context.SaveChangesAsync();
            return true;
        }

        // Retrieves a participation record by its ID
        public async Task<ParticipationDto> GetParticipationByIdAsync(int participationId)
        {
            // Query participation and include user and event details
            var participation = _context.EventParticipants
                .Include(p => p.User)
                .Include(p => p.Event)
                .FirstOrDefaultAsync(p => p.Id == participationId);
            if (participation == null)
                return null;
            // Map and return the participation DTO
            return _mapper.Map<ParticipationDto>(participation);
        }
    }
}
