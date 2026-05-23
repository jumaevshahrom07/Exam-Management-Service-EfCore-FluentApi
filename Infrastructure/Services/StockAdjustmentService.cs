using Domain.Entities;
using Infrastructure.DTOs.StockAdjustmentDto;
using Infrastructure.Interfaces;
using Infrastructure.Persistence.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services;

public class StockAdjustmentService : IStockAdjustmentService
{
    private readonly AppDbContext _context;
    private readonly ILogger<StockAdjustmentService> _logger;

    public StockAdjustmentService(AppDbContext context, ILogger<StockAdjustmentService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<List<GetStockAdjustmentDto>> GetAllAsync()
    {
        try
        {
            return await _context.StockAdjustments.AsNoTracking()
                .Select(s => new GetStockAdjustmentDto
                {
                    Id = s.Id,
                    ProductId = s.ProductId,
                    AdjustmentAmount = s.AdjustmentAmount,
                    Reason = s.Reason,
                    AdjustmentDate = DateTime.UtcNow
                }).ToListAsync();
        }

        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while getting stock adjustments");
            return [];
        }
    }

    public async Task<int> CreateStockAdjustmentAsync(CreateStockAdjustmentDto dto)
    {
        try
        {
            if (dto.ProductId <= 0)
            {
                _logger.LogWarning("Invalid product id");
                return 0;
            }

            if (dto.AdjustmentAmount == 0)
            {
                _logger.LogWarning("Adjustment amount cannot be 0");
                return 0;
            }

            if (string.IsNullOrWhiteSpace(dto.Reason))
            {
                _logger.LogWarning("Reason is required");
                return 0;
            }

            if (dto.Reason.Length > 100)
            {
                _logger.LogWarning("Reason cannot exceed 100 characters");
                return 0;
            }

            var productExists = await _context.Products.AnyAsync(p => p.Id == dto.ProductId);

            if (!productExists)
            {
                _logger.LogWarning("Product not found");
                return 0;
            }

            var adjustment = new StockAdjustment
            {
                ProductId = dto.ProductId,
                AdjustmentAmount = dto.AdjustmentAmount,
                Reason = dto.Reason,
                AdjustmentDate = DateTime.UtcNow
            };

            await _context.StockAdjustments.AddAsync(adjustment);
            return await _context.SaveChangesAsync();
        }

        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while creating stock adjustment");
            return 0;
        }
    }

    public async Task<int> UpdateStockAdjustmentAsync(int id, UpdateStockAdjustmentDto dto)
    {
        try
        {
            if (id <= 0)
            {
                _logger.LogWarning("Invalid stock adjustment id");
                return 0;
            }

            var adjustment = await _context.StockAdjustments.FirstOrDefaultAsync(s => s.Id == id);

            if (adjustment == null)
            {
                _logger.LogWarning("Stock adjustment not found");
                return 0;
            }

            if (dto.ProductId <= 0)
            {
                _logger.LogWarning("Invalid product id");
                return 0;
            }

            if (dto.AdjustmentAmount == 0)
            {
                _logger.LogWarning("Adjustment amount cannot be 0");
                return 0;
            }

            if (string.IsNullOrWhiteSpace(dto.Reason))
            {
                _logger.LogWarning("Reason is required");
                return 0;
            }

            if (dto.Reason.Length > 100)
            {
                _logger.LogWarning("Reason cannot exceed 100 characters");
                return 0;
            }

            var productExists = await _context.Products.AnyAsync(p => p.Id == dto.ProductId);

            if (!productExists)
            {
                _logger.LogWarning("Product not found");
                return 0;
            }

            adjustment.ProductId = dto.ProductId;
            adjustment.AdjustmentAmount = dto.AdjustmentAmount;
            adjustment.Reason = dto.Reason;
            adjustment.AdjustmentDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return 1;
        }

        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while updating stock adjustment");
            return 0;
        }

    }

    public async Task<int> DeleteStockAdjustmentAsync(int id)
    {
        try
        {
            if (id <= 0)
            {
                _logger.LogWarning("Invalid stock adjustment id");
                return 0;
            }

            var adjustment = await _context.StockAdjustments.FindAsync(id);

            if (adjustment == null)
            {
                _logger.LogWarning("Stock adjustment not found");
                return 0;
            }

            _context.StockAdjustments.Remove(adjustment);

            await _context.SaveChangesAsync();
            return 1;
        }

        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while deleting stock adjustment");
            return 0;
        }
    }

    public async Task<GetStockAdjustmentDto?> GetByIdAsync(int id)
    {
        try
        {
            if (id <= 0)
            {
                _logger.LogWarning("Invalid stock adjustment id");
                return null;
            }

            return await _context.StockAdjustments.AsNoTracking()
                .Select(s => new GetStockAdjustmentDto
                {
                    Id = s.Id,
                    ProductId = s.ProductId,
                    AdjustmentAmount = s.AdjustmentAmount,
                    Reason = s.Reason,
                    AdjustmentDate = DateTime.UtcNow
                }).FirstOrDefaultAsync(s => s.Id == id);
        }

        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while getting stock adjustment by id");
            return null;
        }
    }
}