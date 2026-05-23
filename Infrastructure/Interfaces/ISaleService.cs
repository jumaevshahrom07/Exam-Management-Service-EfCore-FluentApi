using Infrastructure.DTOs.SaleDto;

namespace Infrastructure.Interfaces;

public interface ISaleService
{
    Task<List<GetSaleDto>> GetAllAsync();
    Task<int> CreateSaleAsync(CreateSaleDto dto);
    Task<int> UpdateSaleAsync(int id, UpdateSaleDto dto);
    Task<int> DeleteSaleAsync(int id);
    Task<GetSaleDto?> GetByIdAsync(int id);
}