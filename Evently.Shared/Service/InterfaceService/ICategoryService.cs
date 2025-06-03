using Evently.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evently.Shared.Service.InterfaceService
{
    public interface ICategoryService
    {
        // Gets all categories asynchronously
        Task<IEnumerable<CategoryDto>> GetAllAsync();

        // Gets a category by its ID asynchronously
        Task<CategoryDto?> GetByIdAsync(int id);

        // Creates a new category asynchronously
        Task<CategoryDto> CreateAsync(CategoryDto dto);

        // Updates an existing category by its ID asynchronously
        Task<CategoryDto> UpdateAsync(int id, CategoryDto dto);

        // Deletes a category by its ID asynchronously
        Task<bool> DeleteAsync(int id);
    }
}
