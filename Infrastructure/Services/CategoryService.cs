using Domain.Entities;
using Infrastructure.DTOs.CategoryDto;
using Infrastructure.Interfaces;
using Infrastructure.Persistence.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services;

public class CategoryService : ICategoryService
{
    private readonly AppDbContext _context;
    private readonly ILogger<CategoryService> _logger;

    public CategoryService(AppDbContext context, ILogger<CategoryService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<List<GetCategoryDto>> GetAllAsync()
    {
        try
        {
            return await _context.Categories.AsNoTracking().Select(c => new GetCategoryDto
            {
                Id = c.Id,
                Name = c.Name
            }).ToListAsync();
        }

        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while getting category");
            return [];
        }
    }

    public async Task<int> CreateCategoryAsync(CreateCategoryDto dto)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                _logger.LogWarning("Category name is required");
                return 0;
            }

            if (dto.Name.Length < 3 || dto.Name.Length > 50)
            {
                _logger.LogWarning("Category name must be between 3 and 50 characters");
                return 0;
            }

            var exists = await _context.Categories.AnyAsync(c => c.Name == dto.Name);

            if (exists)
            {
                _logger.LogWarning("Category already exists");
                return 0;
            }

            var category = new Category
            {
                Name = dto.Name
            };

            await _context.Categories.AddAsync(category);
            return await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while creating category");
            return 0;
        }
    }

    public async Task<int> UpdateCategoryAsync(int id, UpdateCategoryDto dto)
    {
        try
        {
            if (id <= 0)
            {
                _logger.LogWarning("Invalid category id");
                return 0;
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                _logger.LogWarning("Category not found");
                return 0;
            }

            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                _logger.LogWarning("Category name is required");
                return 0;
            }

            if (dto.Name.Length < 3 || dto.Name.Length > 50)
            {
                _logger.LogWarning("Category name must be between 3 and 50 characters");
                return 0;
            }

            var exists = await _context.Categories
                .AnyAsync(c => c.Name == dto.Name && c.Id != id);

            if (exists)
            {
                _logger.LogWarning("Category name already exists");
                return 0;
            }

            category.Name = dto.Name;
            return await _context.SaveChangesAsync();
        }

        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while updating category");
            return 0;
        }
    }

    public async Task<int> DeleteCategoryAsync(int id)
    {
        try
        {
            if (id <= 0)
            {
                _logger.LogWarning("Invalid category id");
                return 0;
            }

            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                _logger.LogWarning("Category not found");
                return 0;
            }

            _context.Categories.Remove(category);
            return await _context.SaveChangesAsync();
        }

        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while deleting category");
            return 0;
        }
    }

    public async Task<GetCategoryDto?> GetByIdAsync(int id)
    {
        try
        {
            if (id <= 0)
            {
                _logger.LogWarning("Invalid category id");
                return null;
            }

            return await _context.Categories.AsNoTracking()
                .Select(c => new GetCategoryDto
                {
                    Id = c.Id,
                    Name = c.Name
                }).FirstOrDefaultAsync(c => c.Id == id);
        }

        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while getting category by id");
            return null;
        }
    }
}