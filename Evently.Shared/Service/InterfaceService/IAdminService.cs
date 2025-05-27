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
        Task<IEnumerable<UserDto>> GetAllUserAsync();
        Task<bool> PromoteToAdminAsync(int userId);
        Task<bool> DeleteUserAsync(int userId);
        Task<IEnumerable<EventDto>> GetAllEventsAsync();
        Task<IEnumerable<CategoryDto>> GetAllCategoryesAsync();
        Task<bool> RemoveParticipationAsync(int participationId);
    }
}
