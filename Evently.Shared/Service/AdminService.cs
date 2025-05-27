using AutoMapper;
using BackendEvently.Data;
using BackendEvently.Dtos;
using BackendEvently.Model;
using Evently.Shared.Dtos;
using Evently.Shared.Service.InterfaceService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evently.Shared.Service
{
    public class AdminService : IAdminService
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public AdminService(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return false;
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategoryesAsync()
        {
            var categorys = await _context.Categoryes.ToListAsync();
            return _mapper.Map<IEnumerable<CategoryDto>>(categorys);
        }

        public async Task<IEnumerable<EventDto>> GetAllEventsAsync()
        {
            var events = await _context.Events.Include(e=>e.Category).ToListAsync();
            return _mapper.Map<IEnumerable<EventDto>>(events);
        }

        public async Task<IEnumerable<UserDto>> GetAllUserAsync()
        {
            var users=await _context.Users.ToListAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<bool> PromoteToAdminAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return false;
            user.Role = "Admin";
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveParticipationAsync(int participationId)
        {
            var participation = await _context.EventParticipants.FindAsync(participationId);
            if (participation == null) return false;
            _context.EventParticipants.Remove(participation);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
