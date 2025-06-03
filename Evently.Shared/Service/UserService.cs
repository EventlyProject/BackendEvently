 using AutoMapper;
using BackendEvently.Data;
using BackendEvently.Dtos;
using BackendEvently.Model;
using Evently.Shared.Dtos;
using Evently.Shared.Service.InterfaceService;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;

namespace Evently.Shared.Service
{
    public class UserService : IUserService
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        // Constructor for UserService, injects the database context and AutoMapper
        public UserService(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Retrieves all users from the database and maps them to UserDto
        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _context.Users.ToListAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        // Retrieves a user by their ID and maps to UserDto, returns null if not found
        public async Task<UserDto?> GetByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            return user == null ? null : _mapper.Map<UserDto>(user);
        }

        // Registers a new user, hashes the password, and returns the created UserDto
        public async Task<UserDto> RegisterAsync(RegisterDto dto)
        {
            // Check if a user with the same email already exists
            var exist = await _context.Users.AnyAsync(u => u.Emailaddress == dto.Emailaddress);
            if (exist) throw new Exception("Email already registered");

            // Hash the user's password
            var hashedPassword = HashPassword(dto.Password);

            // Create a new User entity
            var user = new User
            {
                Username = dto.Username,
                Emailaddress = dto.Emailaddress,
                PasswordHash = hashedPassword
            };

            // Add the new user to the database
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return _mapper.Map<UserDto>(user);
        }
        // Updates an existing user's information, including password if provided
        public async Task<UserDto?> UpdateUserAsync(int Id, UpdateUserDto dto)
        {
            var user = await _context.Users.FindAsync(Id);
            if (user == null) return null;

            // Update username and email
            user.Username = dto.Username;
            user.Emailaddress = dto.Emailaddress;

            // Update password if a new one is provided
            if (!string.IsNullOrWhiteSpace(dto.Password))
            {
                user.PasswordHash = HashPassword(dto.Password);
            }
            await _context.SaveChangesAsync();
            return _mapper.Map<UserDto>(user);
        }
        // Deletes a user by ID, returns true if successful, false if user not found
        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
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
