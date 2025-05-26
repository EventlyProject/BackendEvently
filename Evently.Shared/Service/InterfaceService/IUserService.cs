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
        Task<IEnumerable<UserDto>>GetAllAsync();
        Task<UserDto?>GetByIdAsync(int id);
        Task<UserDto> RegisterAsync(RegisterDto dto);
    }
}
