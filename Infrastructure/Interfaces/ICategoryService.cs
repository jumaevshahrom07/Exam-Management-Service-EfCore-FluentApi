using Infrastructure.DTOs.CategoryDto;

namespace Infrastructure.Interfaces;

public interface ICategoryService
{
    Task<List<GetCategoryDto>> GetAllAsync();
    Task<int> CreateCategoryAsync(CreateCategoryDto dto);
    Task<int> UpdateCategoryAsync(int id, UpdateCategoryDto dto);
    Task<int> DeleteCategoryAsync(int id);
    Task<GetCategoryDto?> GetByIdAsync(int id);
}