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
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public EventService(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EventDto>> GetAllAsync()
        {
            var events = await _context.Events.Include(e=>e.Category).ToListAsync();
            return _mapper.Map<IEnumerable<EventDto>>(events);
        }

        public async Task<EventDto?> GetByIdAsync(int id)
        {
            var evt = await _context.Events.Include(e => e.Category).FirstOrDefaultAsync(e => e.Id == id);
            return evt == null ? null : _mapper.Map<EventDto>(evt);
        }

        public async Task<EventDto>CreateAsync(EventDto dto)
        {
            var evt = _mapper.Map<Event>(dto);
            _context.Events.Add(evt);
            await _context.SaveChangesAsync();

            await _context.Entry(evt).Reference(e=>e.Category).LoadAsync();
            return _mapper.Map<EventDto>(evt);
        }

        public async Task<EventDto?> UpdateAsync(int id,EventDto dto)
        {
            var evt = await _context.Events.FirstOrDefaultAsync(e => e.Id == id);
            if (evt == null) return null;

            if(evt.UserId != dto.UserId)
            {   
                throw new UnauthorizedAccessException("You are not authorized to update this event.");
            }
            _mapper.Map(dto, evt);
            await _context.SaveChangesAsync();

            await _context.Entry(evt).Reference(e => e.Category).LoadAsync();
            return _mapper.Map<EventDto>(evt);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var evt = await _context.Events.FirstOrDefaultAsync(e=>e.Id == id);
            if (evt == null) return false;

            _context.Events.Remove(evt);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
