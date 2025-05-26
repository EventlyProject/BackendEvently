using BackendEvently.Data;
using BackendEvently.Dtos;
using BackendEvently.Model;
using Evently.Shared.Dtos;
using Evently.Shared.Service.InterfaceService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackendEvently.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var events = await _eventService.GetAllAsync();
            return Ok(events);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var evt = await _eventService.GetByIdAsync(id);
            if(evt==null) return NotFound();
            return Ok(evt);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEventDto dto)
        {
            var created = await _eventService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,CreateEventDto dto)
        {
            var updated = await _eventService.UpdateAsync(id,dto);
            if(updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _eventService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
