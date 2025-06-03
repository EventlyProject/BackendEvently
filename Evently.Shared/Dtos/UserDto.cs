using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evently.Shared.Dtos
{
    // Data Transfer Object for user information
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Emailaddress { get; set; } = string.Empty;
        public string Role { get; set; } = "User";
    }
    // Data Transfer Object for updating user information
    public class UpdateUserDto
    {
        public string Username { get; set; } = string.Empty;
        public string Emailaddress { get; set; } = string.Empty;
        public string? Password { get; set; }
    }
}
