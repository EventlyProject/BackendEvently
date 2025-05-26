using BackendEvently.Data;
using BackendEvently.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Evently.Shared.Service.InterfaceService;
using Microsoft.AspNetCore.Http.HttpResults;
using Evently.Shared.Dtos;

namespace BackendEvently.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [ HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categoryes=await _categoryService.GetAllAsync();
            return Ok(categoryes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult>GetById(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if(category == null) return NotFound();
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryDto dto)
        {
            var created = await _categoryService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult>Update(int id, CategoryDto dto)
        {
            var updated = await _categoryService.UpdateAsync(id,dto);
            if(updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult>Delete(int id)
        {
            var deleted = await _categoryService.DeleteAsync(id);
            if(!deleted)return NotFound();
            return Ok(deleted);
        }
    }
}
