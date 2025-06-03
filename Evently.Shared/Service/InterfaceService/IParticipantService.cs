using BackendEvently.Dtos;
using Evently.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evently.Shared.Service.InterfaceService
{
    public interface IParticipantService
    {
        Task<ParticipationDto> RegisterAsync(int userId, int eventId);
        Task<IEnumerable<UserDto>> GetParticipantsByEventIdAsync(int eventId);
        Task<IEnumerable<EventDto>>GetEventsByUserIdAsync(int userId);
        Task<bool> RemoveParticipationAsync(int participationId);
        Task<ParticipationDto> GetParticipationByIdAsync(int participationId);
    }
}
