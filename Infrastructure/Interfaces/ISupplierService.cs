using Infrastructure.DTOs.SupplierDto;

namespace Infrastructure.Interfaces;

public interface ISupplierService
{
    Task<List<GetSupplierDto>> GetAllAsync();
    Task<int> CreateSupplierAsync(CreateSupplierDto dto);
    Task<int> UpdateSupplierAsync(int id, UpdateSupplierDto dto);
    Task<int> DeleteSupplierAsync(int id);
    Task<GetSupplierDto?> GetByIdAsync(int id);
}