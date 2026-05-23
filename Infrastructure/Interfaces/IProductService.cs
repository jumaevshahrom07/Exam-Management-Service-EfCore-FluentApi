using Infrastructure.DTOs.ProductDto;

namespace Infrastructure.Interfaces;

public interface IProductService
{
    Task<List<GetProductDto>> GetAllAsync();
    Task<int> CreateProductAsync(CreateProductDto dto);
    Task<int> UpdateProductAsync(int id, UpdateProductDto dto);
    Task<int> DeleteProductAsync(int id);
    Task<GetProductDto?> GetByIdAsync(int id);
}