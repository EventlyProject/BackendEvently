using AutoMapper;
using BackendEvently.Data;
using BackendEvently.Dtos;
using BackendEvently.Model;
using Evently.Shared.Dtos;
using Evently.Shared.Service.InterfaceService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BackendEvently.Service
{
    public class JwtService : IJwtService
    {
        private readonly ApplicationDBContext _context;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        // Constructor to inject dependencies: database context, configuration, and mapper
        public JwtService(ApplicationDBContext context, IConfiguration configuration, IMapper mapper)
        {
            _context = context;
            _configuration = configuration;
            _mapper = mapper;
        }

        // Authenticates a user by email and password, returns UserDto if successful, otherwise null
        public async Task<UserDto?> AuthenticateAsync(LoginDto dto)
        {
            // Hash the provided password for comparison
            var hashedPassword = HashPassword(dto.Password);

            // Attempt to find a user with the matching email and hashed password
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Emailaddress == dto.Emailaddress && u.PasswordHash == hashedPassword);

            // If user is found, map to UserDto, otherwise return null
            return user == null ? null : _mapper.Map<UserDto>(user);
        }

        // Generates a JWT token for the given user
        public string GenerateToken(UserDto user)
        {
            // Create a symmetric security key from the configuration
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

            // Define claims to include in the token
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Emailaddress),
                new Claim(ClaimTypes.Role, user.Role)
            };

            // Create the JWT token with issuer, audience, claims, expiration, and signing credentials
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            // Return the serialized JWT token
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // Hashes a password using SHA256 and returns the base64 string
        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
