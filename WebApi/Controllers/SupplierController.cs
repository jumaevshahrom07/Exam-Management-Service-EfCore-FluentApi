using Infrastructure.DTOs.SupplierDto;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/suppliers")]
public class SupplierController : ControllerBase
{
    private readonly ISupplierService _service;

    public SupplierController(ISupplierService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateSupplierDto dto)
    {
        var result = await _service.CreateSupplierAsync(dto);

        if (result == 0)
        {
            return BadRequest();
        }
        return Ok("Created successfully");
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateSupplierDto dto)
    {
        var result = await _service.UpdateSupplierAsync(id, dto);

        if (result == 0)
        {
            return BadRequest();
        }
        return Ok("Updated successfully");
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.DeleteSupplierAsync(id);

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