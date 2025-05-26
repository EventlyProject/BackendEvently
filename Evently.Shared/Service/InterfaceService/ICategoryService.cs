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
        Task<IEnumerable<CategoryDto>> GetAllAsync();
        Task<CategoryDto?> GetByIdAsync(int id);
        Task<CategoryDto> CreateAsync(CategoryDto dto);
        Task<CategoryDto> UpdateAsync(int id, CategoryDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
