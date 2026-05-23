using Infrastructure.DTOs.StockAdjustmentDto;

namespace Infrastructure.Interfaces;

public interface IStockAdjustmentService
{
    Task<List<GetStockAdjustmentDto>> GetAllAsync();
    Task<int> CreateStockAdjustmentAsync(CreateStockAdjustmentDto dto);
    Task<int> UpdateStockAdjustmentAsync(int id, UpdateStockAdjustmentDto dto);
    Task<int> DeleteStockAdjustmentAsync(int id);
    Task<GetStockAdjustmentDto?> GetByIdAsync(int id);
}