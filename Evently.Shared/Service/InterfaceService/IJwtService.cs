using BackendEvently.Dtos;
using BackendEvently.Model;
using Evently.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evently.Shared.Service.InterfaceService
{
    public interface IJwtService
    {
        string GenerateToken(UserDto user);
        Task<UserDto> AuthenticateAsync(LoginDto dto);
    }
}
