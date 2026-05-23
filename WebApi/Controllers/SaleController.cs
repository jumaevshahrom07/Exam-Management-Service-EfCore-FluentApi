using Infrastructure.DTOs.SaleDto;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/sales")]
public class SaleController : ControllerBase
{
    private readonly ISaleService _service;

    public SaleController(ISaleService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateSaleDto dto)
    {
        var result = await _service.CreateSaleAsync(dto);

        if (result == 0)
        {
            return BadRequest();
        }
        return Ok("Created successfully");
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateSaleDto dto)
    {
        var result = await _service.UpdateSaleAsync(id, dto);

        if (result == 0)
        {  
            return BadRequest();
        }
        return Ok("Updated successfully");
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.DeleteSaleAsync(id);

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