using BackendEvently.Dtos;
using Evently.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evently.Shared.Service.InterfaceService
{
    public interface IUserService
    {
        // Gets all users asynchronously
        Task<IEnumerable<UserDto>> GetAllAsync();

        // Gets a user by their ID asynchronously
        Task<UserDto?> GetByIdAsync(int id);

        // Registers a new user asynchronously
        Task<UserDto> RegisterAsync(RegisterDto dto);

        // Deletes a user by their ID asynchronously
        Task<bool> DeleteUserAsync(int id);

        // Updates a user's information by their ID asynchronously
        Task<UserDto?> UpdateUserAsync(int id, UpdateUserDto dto);
    }
}
