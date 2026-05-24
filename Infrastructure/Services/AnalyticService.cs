using Infrastructure.AnalyticDto;
using Infrastructure.Interfaces;
using Infrastructure.Persistence.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services;

public class AnalyticService : IAnalyticService
{
    private readonly AppDbContext _context;
    private readonly ILogger<AnalyticService> _logger;

    public AnalyticService(AppDbContext context, ILogger<AnalyticService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<List<CategoryWithProductsDto>> GetCategoriesWithProductsAsync()
    {
        return await _context.Categories.AsNoTracking()
            .Select(c => new CategoryWithProductsDto
            {
                CategoryId = c.Id,
                CategoryName = c.Name,
                Products = c.Products.Select(p => new ProductShortDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price
                }).ToList()
            }).ToListAsync();
    }

    public async Task<List<LowStockDto>> GetLowStockProductsAsync()
    {
        return await _context.Products.Where(p => p.QuantityInStock < 5)
            .Select(p => new LowStockDto
            {
                Id = p.Id,
                Name = p.Name,
                QuantityInStock = p.QuantityInStock
            }).ToListAsync();
    }

    public async Task<ProductStatisticsDto> GetProductStatisticsAsync()
    {
        return new ProductStatisticsDto
        {
            TotalProducts = await _context.Products.CountAsync(),
            AveragePrice = await _context.Products.AverageAsync(p => p.Price),
            TotalSold = await _context.Sales.SumAsync(s => s.QuantitySold)
        };
    }

    public async Task<List<SalesByDateDto>> GetSalesByDateAsync(DateTime start, DateTime end)
    {
        return await _context.Sales.Where(s => s.SaleDate >= start && s.SaleDate <= end)
            .Select(s => new SalesByDateDto
            {
                SaleId = s.Id,
                ProductName = s.Product.Name,
                QuantitySold = s.QuantitySold,
                SaleDate = s.SaleDate
            }).ToListAsync();
    }

    public async Task<List<TopProductsDto>> GetTopProductsAsync()
    {
        return await _context.Sales.GroupBy(s => s.Product.Name)
            .Select(g => new TopProductsDto
            {
                ProductName = g.Key,
                TotalSold = g.Sum(x => x.QuantitySold)
            }).OrderByDescending(x => x.TotalSold).Take(5).ToListAsync();
    }

    public async Task<List<DailyRevenueDto>> GetDailyRevenueAsync()
    {
        return await _context.Sales.GroupBy(s => s.SaleDate.Date)
            .Select(g => new DailyRevenueDto
            {
                Date = g.Key,
                Revenue = g.Sum(x => x.QuantitySold * x.Product.Price)
            }).ToListAsync();
    }

    public async Task<List<StockHistoryDto>> GetStockHistoryAsync(int productId)
    {
        return await _context.StockAdjustments.Where(x => x.ProductId == productId)
            .Select(x => new StockHistoryDto
            {
                AdjustmentDate = x.AdjustmentDate,
                Amount = x.AdjustmentAmount,
                Reason = x.Reason
            }).ToListAsync();
    }

    public async Task<ProductDetailsDto?> GetProductDetailsAsync(int id)
    {
        return await _context.Products.Where(p => p.Id == id)
            .Select(p => new ProductDetailsDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                QuantityInStock = p.QuantityInStock,
                Category = p.Category.Name,
                Supplier = p.Supplier.Name,

                Sales = p.Sales.Select(s => new SaleMiniDto
                {
                    Quantity = s.QuantitySold,
                    Date = s.SaleDate
                }).ToList(),

                Adjustments = p.StockAdjustments.Select(a => new StockMiniDto
                {
                    Amount = a.AdjustmentAmount,
                    Date = a.AdjustmentDate,
                    Reason = a.Reason
                }).ToList()
            }).FirstOrDefaultAsync();
    }

    public async Task<DashboardDto> GetDashboardStatisticsAsync()
    {
        return new DashboardDto
        {
            TotalProducts = await _context.Products.CountAsync(),
            TotalRevenue = await _context.Sales.SumAsync(s => s.QuantitySold * s.Product.Price),
            TotalSales = await _context.Sales.CountAsync()
        };
    }

    public async Task<List<SupplierWithProductsDto>> GetSuppliersWithProductsAsync()
    {
        return await _context.Suppliers.Select(s => new SupplierWithProductsDto
        {
            SupplierId = s.Id,
            SupplierName = s.Name,
            Products = s.Products.Select(p => p.Name).ToList()
        }).ToListAsync();
    }
}