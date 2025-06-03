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

        // Constructor to inject the database context and AutoMapper
        public AdminService(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Deletes a user by their ID. Returns true if successful, false if user not found.
        public async Task<bool> DeleteUserAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return false;
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        // Retrieves all categories from the database and maps them to DTOs.
        public async Task<IEnumerable<CategoryDto>> GetAllCategoryesAsync()
        {
            var categorys = await _context.Categoryes.ToListAsync();
            return _mapper.Map<IEnumerable<CategoryDto>>(categorys);
        }

        // Retrieves all events, including their categories, and maps them to DTOs.
        public async Task<IEnumerable<EventDto>> GetAllEventsAsync()
        {
            var events = await _context.Events.Include(e => e.Category).ToListAsync();
            return _mapper.Map<IEnumerable<EventDto>>(events);
        }

        // Retrieves all users and maps them to DTOs.
        public async Task<IEnumerable<UserDto>> GetAllUserAsync()
        {
            var users = await _context.Users.ToListAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        // Promotes a user to admin by setting their role. Returns true if successful.
        public async Task<bool> PromoteToAdminAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return false;
            user.Role = "Admin";
            await _context.SaveChangesAsync();
            return true;
        }

        //Removes a participation record by its ID.Returns true if successful.
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
