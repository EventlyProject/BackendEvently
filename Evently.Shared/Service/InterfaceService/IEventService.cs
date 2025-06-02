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
        Task<IEnumerable<EventDto>> GetAllAsync();
        Task<EventDto?>GetByIdAsync(int id);
        Task<EventDto> CreateAsync(EventDto dto);
        Task<EventDto?>UpdateAsync(int id,EventDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
