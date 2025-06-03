using BackendEvently.Dtos;
using Evently.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evently.Shared.Service.InterfaceService
{
    // Interface for participant-related operations
    public interface IParticipantService
    {
        // Registers a user for an event and returns the participation details
        Task<ParticipationDto> RegisterAsync(int userId, int eventId);

        // Gets all users participating in a specific event
        Task<IEnumerable<UserDto>> GetParticipantsByEventIdAsync(int eventId);

        // Gets all events a specific user is participating in
        Task<IEnumerable<EventDto>> GetEventsByUserIdAsync(int userId);

        // Removes a user's participation from an event
        Task<bool> RemoveParticipationAsync(int participationId);

        // Gets participation details by participation ID
        Task<ParticipationDto> GetParticipationByIdAsync(int participationId);
    }
}
