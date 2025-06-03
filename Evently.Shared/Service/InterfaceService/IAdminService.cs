using BackendEvently.Dtos;
using Evently.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evently.Shared.Service.InterfaceService
{
    public interface IAdminService
    {
        // Retrieves all users in the system asynchronously
        Task<IEnumerable<UserDto>> GetAllUserAsync();
        // Promotes a user to admin role by user ID asynchronously
        Task<bool> PromoteToAdminAsync(int userId);
        // Deletes a user by user ID asynchronously
        Task<bool> DeleteUserAsync(int userId);
        // Retrieves all events in the system asynchronously
        Task<IEnumerable<EventDto>> GetAllEventsAsync();
        // Retrieves all categories in the system asynchronously
        Task<IEnumerable<CategoryDto>> GetAllCategoryesAsync();
        // Removes a participation entry by participation ID asynchronously
        Task<bool> RemoveParticipationAsync(int participationId);
    }
}
