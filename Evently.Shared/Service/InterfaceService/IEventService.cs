using BackendEvently.Dtos;
using Evently.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evently.Shared.Service.InterfaceService
{
    public interface IEventService
    {
        // Retrieves all events asynchronously
        Task<IEnumerable<EventDto>> GetAllAsync();

        // Retrieves a specific event by its ID asynchronously
        Task<EventDto?> GetByIdAsync(int id);

        // Creates a new event asynchronously
        Task<EventDto> CreateAsync(EventDto dto);

        // Updates an existing event by its ID asynchronously
        Task<EventDto?> UpdateAsync(int id, EventDto dto);

        // Deletes an event by its ID asynchronously
        Task<bool> DeleteAsync(int id);
    }
}
