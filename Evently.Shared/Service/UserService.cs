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

        public UserService(ApplicationDBContext context, IMapper mapper)
        {
            _context=context;
            _mapper=mapper;
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _context.Users.ToListAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto?> GetByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            return user == null ? null : _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> RegisterAsync(RegisterDto dto)
        {
            // check if user exist
            var exist = await _context.Users.AnyAsync(u=>u.Emailaddress == dto.Emailaddress);
            if (exist) throw new Exception("Email already registered");

            // Hash password
            var hashedPassword = HashPassword(dto.Password);

            var user = new User
            {
                Username = dto.Username,
                Emailaddress = dto.Emailaddress,
                PasswordHash = hashedPassword
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return _mapper.Map<UserDto>(user);
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
