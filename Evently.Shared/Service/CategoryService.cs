using AutoMapper;
using BackendEvently.Data;
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
    // Service class for managing categories
    public class CategoryService : ICategoryService
    {
        // Database context for accessing the database
        private readonly ApplicationDBContext _context;
        // Mapper for converting between entities and DTOs
        private readonly IMapper _mapper;

        // Constructor with dependency injection for context and mapper
        public CategoryService(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Retrieves all categories as DTOs
        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            var categories = await _context.Categoryes.ToListAsync();
            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        // Retrieves a single category by its ID as a DTO
        public async Task<CategoryDto?> GetByIdAsync(int id)
        {
            var category = await _context.Categoryes.FindAsync(id);
            return category == null ? null : _mapper.Map<CategoryDto>(category);
        }

        // Creates a new category from a DTO and returns the created DTO
        public async Task<CategoryDto> CreateAsync(CategoryDto dto)
        {
            var category = _mapper.Map<Category>(dto);
            _context.Categoryes.Add(category);
            await _context.SaveChangesAsync();
            return _mapper.Map<CategoryDto>(category);
        }

        // Updates an existing category by ID using the provided DTO, returns the updated DTO or null if not found
        public async Task<CategoryDto?> UpdateAsync(int id, CategoryDto dto)
        {
            var category = await _context.Categoryes.FindAsync(id);
            if (category == null) return null;

            category.Name = dto.Name;
            await _context.SaveChangesAsync();
            return _mapper.Map<CategoryDto>(category);
        }

        // Deletes a category by ID, returns true if successful, false if not found
        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _context.Categoryes.FindAsync(id);
            if (category == null) return false;

            _context.Categoryes.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
