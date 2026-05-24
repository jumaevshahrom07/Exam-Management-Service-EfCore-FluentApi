using Domain.Entities;
using Infrastructure.DTOs.SaleDto;
using Infrastructure.Interfaces;
using Infrastructure.Persistence.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services;

public class SaleService : ISaleService
{
    private readonly AppDbContext _context;
    private readonly ILogger<SaleService> _logger;

    public SaleService(AppDbContext context, ILogger<SaleService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<List<GetSaleDto>> GetAllAsync()
    {
        try
        {
            return await _context.Sales.AsNoTracking()
                .Select(s => new GetSaleDto
                {
                    Id = s.Id,
                    ProductId = s.ProductId,
                    QuantitySold = s.QuantitySold,
                    SaleDate = DateTime.UtcNow
                }).ToListAsync();
        }

        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while getting sales");
            return [];
        }
    }

    public async Task<int> CreateSaleAsync(CreateSaleDto dto)
    {
        try
        {
            if (dto.ProductId <= 0)
            {
                _logger.LogWarning("Invalid product id");
                return 0;
            }

            if (dto.QuantitySold <= 0)
            {
                _logger.LogWarning("Quantity sold must be greater than 0");
                return 0;
            }
            
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == dto.ProductId);

            if (product == null)
            {
                _logger.LogWarning("Product not found");
                return 0;
            }

            if (product.QuantityInStock < dto.QuantitySold)
            {
                _logger.LogWarning("Not enough products in stock");
                return 0;
            }

            product.QuantityInStock -= dto.QuantitySold;

            var sale = new Sale
            {
                ProductId = dto.ProductId,
                QuantitySold = dto.QuantitySold,
                SaleDate = DateTime.UtcNow
            };

            await _context.Sales.AddAsync(sale);
            return await _context.SaveChangesAsync();
        }

        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while creating sale");
            return 0;
        }
    }

    public async Task<int> UpdateSaleAsync(int id, UpdateSaleDto dto)
    {
        try
        {
            if (id <= 0)
            {
                _logger.LogWarning("Invalid sale id");
                return 0;
            }

            var sale = await _context.Sales.FirstOrDefaultAsync(s => s.Id == id);

            if (sale == null)
            {
                _logger.LogWarning("Sale not found");
                return 0;
            }

            if (dto.ProductId <= 0)
            {
                _logger.LogWarning("Invalid product id");
                return 0;
            }

            if (dto.QuantitySold <= 0)
            {
                _logger.LogWarning("Quantity sold must be greater than 0");
                return 0;
            }

            var productExists = await _context.Products.AnyAsync(p => p.Id == dto.ProductId);

            if (!productExists)
            {
                _logger.LogWarning("Product not found");
                return 0;
            }

            sale.ProductId = dto.ProductId;
            sale.QuantitySold = dto.QuantitySold;
            sale.SaleDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return 1;
        }

        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while updating sale");
            return 0;
        }
    }

    public async Task<int> DeleteSaleAsync(int id)
    {
        try
        {
            if (id <= 0)
            {
                _logger.LogWarning("Invalid sale id");
                return 0;
            }

            var sale = await _context.Sales.FindAsync(id);

            if (sale == null)
            {
                _logger.LogWarning("Sale not found");
                return 0;
            }

            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync();
            return 1;
        }

        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while deleting sale");
            return 0;
        }
    }

    public async Task<GetSaleDto?> GetByIdAsync(int id)
    {
        try
        {
            if (id <= 0)
            {
                _logger.LogWarning("Invalid sale id");
                return null;
            }

            return await _context.Sales.AsNoTracking()
                .Select(s => new GetSaleDto
                {
                    Id = s.Id,
                    ProductId = s.ProductId,
                    QuantitySold = s.QuantitySold,
                    SaleDate = DateTime.UtcNow
                }).FirstOrDefaultAsync(s => s.Id == id);
        }

        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while getting sale by id");
            return null;
        }
    }
}