using Infrastructure.DTOs.StockAdjustmentDto;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/stock-adjustments")]
public class StockAdjustmentController : ControllerBase
{
    private readonly IStockAdjustmentService _service;

    public StockAdjustmentController(IStockAdjustmentService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateStockAdjustmentDto dto)
    {
        var result = await _service.CreateStockAdjustmentAsync(dto);

        if (result == 0)
        {
            return BadRequest();
        }
        return Ok("Created successfully");
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateStockAdjustmentDto dto)
    {
        var result = await _service.UpdateStockAdjustmentAsync(id, dto);

        if (result == 0)
        {
            return BadRequest();
        }
        return Ok("Updated successfully");
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.DeleteStockAdjustmentAsync(id);

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