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
    // Interface for JWT service
    public interface IJwtService
    {
        // Generates a JWT token for the given user
        string GenerateToken(UserDto user);

        // Authenticates a user using login credentials and returns user data
        Task<UserDto> AuthenticateAsync(LoginDto dto);
    }
}
