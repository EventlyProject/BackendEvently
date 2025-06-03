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
    public class EventService : IEventService
    {
        // The application's Entity Framework Core database context
        private readonly ApplicationDBContext _context;
        // AutoMapper instance for mapping between entities and DTOs
        private readonly IMapper _mapper;

        // Constructor injecting the database context and AutoMapper
        public EventService(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Retrieves all events with their categories, maps them to DTOs
        public async Task<IEnumerable<EventDto>> GetAllAsync()
        {
            var events = await _context.Events.Include(e => e.Category).ToListAsync();
            return _mapper.Map<IEnumerable<EventDto>>(events);
        }

        // Retrieves a single event by ID, including its category, and maps to DTO
        public async Task<EventDto?> GetByIdAsync(int id)
        {
            var evt = await _context.Events.Include(e => e.Category).FirstOrDefaultAsync(e => e.Id == id);
            return evt == null ? null : _mapper.Map<EventDto>(evt);
        }

        // Creates a new event from a DTO, saves it, loads its category, and returns the mapped DTO
        public async Task<EventDto> CreateAsync(EventDto dto)
        {
            var evt = _mapper.Map<Event>(dto);
            _context.Events.Add(evt);
            await _context.SaveChangesAsync();

            // Ensure the Category navigation property is loaded
            await _context.Entry(evt).Reference(e => e.Category).LoadAsync();
            return _mapper.Map<EventDto>(evt);
        }

        // Updates an existing event if the user is authorized, saves changes, loads category, and returns the mapped DTO
        public async Task<EventDto?> UpdateAsync(int id, EventDto dto)
        {
            var evt = await _context.Events.FirstOrDefaultAsync(e => e.Id == id);
            if (evt == null) return null;

            // Only the event owner can update the event
            if (evt.UserId != dto.UserId)
            {
                throw new UnauthorizedAccessException("You are not authorized to update this event.");
            }
            _mapper.Map(dto, evt);
            await _context.SaveChangesAsync();

            // Ensure the Category navigation property is loaded
            await _context.Entry(evt).Reference(e => e.Category).LoadAsync();
            return _mapper.Map<EventDto>(evt);
        }

        // Deletes an event by ID, returns true if successful
        public async Task<bool> DeleteAsync(int id)
        {
            var evt = await _context.Events.FirstOrDefaultAsync(e => e.Id == id);
            if (evt == null) return false;

            _context.Events.Remove(evt);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
