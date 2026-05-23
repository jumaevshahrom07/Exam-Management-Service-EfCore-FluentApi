using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/analytics")]
public class AnalyticController : ControllerBase
{
    private readonly IAnalyticService _service;

    public AnalyticController(IAnalyticService service)
    {
        _service = service;
    }

    [HttpGet("categories-with-products")]
    public async Task<IActionResult> GetCategories()
    {
        return Ok(await _service.GetCategoriesWithProductsAsync());
    }

    [HttpGet("low-stock")]
    public async Task<IActionResult> GetLowStock()
    {
        return Ok(await _service.GetLowStockProductsAsync());
    }

    [HttpGet("statistics")]
    public async Task<IActionResult> GetStatistics()
    {
        return Ok(await _service.GetProductStatisticsAsync());
    }

    [HttpGet("sales-by-date")]
    public async Task<IActionResult> GetSalesByDate(DateTime fromDate, DateTime toDate)
    {
        return Ok(await _service.GetSalesByDateAsync(fromDate, toDate));
    }

    [HttpGet("top-products")]
    public async Task<IActionResult> GetTopProducts()
    {
        return Ok(await _service.GetTopProductsAsync());
    }

    [HttpGet("daily-revenue")]
    public async Task<IActionResult> GetDailyRevenue()
    {
        return Ok(await _service.GetDailyRevenueAsync());
    }

    [HttpGet("stock-history")]
    public async Task<IActionResult> GetStockHistory(int productId)
    {
        return Ok(await _service.GetStockHistoryAsync(productId));
    }

    [HttpGet("product-details/{id:int}")]
    public async Task<IActionResult> GetProductDetails(int id)
    {
        var result = await _service.GetProductDetailsAsync(id);

        if (result == null)
        {
            return BadRequest();
        }

        return Ok(result);
    }

    [HttpGet("suppliers-with-products")]
    public async Task<IActionResult> GetSuppliers()
    {
        return Ok(await _service.GetSuppliersWithProductsAsync());
    }

    [HttpGet("dashboard")]
    public async Task<IActionResult> GetDashboard()
    {
        return Ok(await _service.GetDashboardStatisticsAsync());
    }
}