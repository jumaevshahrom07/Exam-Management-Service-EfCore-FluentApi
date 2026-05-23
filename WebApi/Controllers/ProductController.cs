using Infrastructure.DTOs.ProductDto;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/products")]

public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetProductAsycn()
    {
        return Ok(await _productService.GetAllAsync());
    }

    [HttpPost]
    public async Task<IActionResult> CreateProductAsync(CreateProductDto dto)
    {
        var result = await _productService.CreateProductAsync(dto);

        if (result == 0)
        {
            return BadRequest();
        }

        return Ok("Created successfully");
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateProductAsync(int id, UpdateProductDto dto)
    {
        var result = await _productService.UpdateProductAsync(id, dto);

        if (result == 0)
        {
            return BadRequest();
        }

        return Ok("Updated succesfully");
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteProductAsync(int id)
    {
        var result = await _productService.DeleteProductAsync(id);

        if (result == 0)
        {
            return BadRequest();
        }

        return Ok("Deleted successfully");
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var result = await _productService.GetByIdAsync(id);
        if (result == null)
        {
            return BadRequest();
        }

        return Ok("Getted successfully");
    }
}