using Infrastructure.DTOs.CategoryDto;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _service;

    public CategoryController(ICategoryService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllAsync();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCategoryDto dto)
    {
        var result = await _service.CreateCategoryAsync(dto);

        if (result == 0)
        {
            return BadRequest();
        }
        return Ok("Created successfully");
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateCategoryDto dto)
    {
        var result = await _service.UpdateCategoryAsync(id, dto);

        if (result == 0)
        {
            return BadRequest();
        }
        return Ok("Updated successfully");
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.DeleteCategoryAsync(id);

        if (result == 0)
        {
            return NotFound();
        }
        return Ok("Deleted successfully");
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);

        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }
}