using Domain.Entities;
using Infrastructure.DTOs.ProductDto;
using Infrastructure.Interfaces;
using Infrastructure.Persistence.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services;

public class ProductService : IProductService
{
    private readonly AppDbContext _context;
    private readonly ILogger<ProductService> _logger;

    public ProductService(AppDbContext context, ILogger<ProductService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<List<GetProductDto>> GetAllAsync()
    {
        try
        {
            return await _context.Products.AsNoTracking().Select(p => new GetProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                QuantityInStock = p.QuantityInStock,
                CategoryId = p.CategoryId,
                SupplierId = p.SupplierId
            }).ToListAsync();
        }

        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while getting products");
            return [];
        }
    }

    public async Task<int> CreateProductAsync(CreateProductDto dto)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                _logger.LogWarning("Product name is required");
                return 0;
            }

            if (dto.Name.Length < 3 || dto.Name.Length > 50)
            {
                _logger.LogWarning("Product name must be between 3 and 50 characters");
                return 0;
            }

            if (dto.Price <= 0)
            {
                _logger.LogWarning("Price must be greater than 0");
                return 0;
            }

            if (dto.QuantityInStock < 0)
            {
                _logger.LogWarning("Quantity in stock cannot be negative");
                return 0;
            }

            var categoryExists = await _context.Categories.AnyAsync(x => x.Id == dto.CategoryId);

            if (!categoryExists)
            {
                _logger.LogWarning("Category not found");
                return 0;
            }

            var supplierExists = await _context.Suppliers.AnyAsync(x => x.Id == dto.SupplierId);

            if (!supplierExists)
            {
                _logger.LogWarning("Supplier not found");
                return 0;
            }

            var product = new Product
            {
                Name = dto.Name,
                Price = dto.Price,
                QuantityInStock = dto.QuantityInStock,
                CategoryId = dto.CategoryId,
                SupplierId = dto.SupplierId
            };

            await _context.Products.AddAsync(product);
            return await _context.SaveChangesAsync();
        }

        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while creating product");
            return 0;
        }
    }

    public async Task<int> UpdateProductAsync(int id, UpdateProductDto dto)
    {
        try
        {
            if (id <= 0)
            {
                _logger.LogWarning("Invalid input");
                return 0;
            }

            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                _logger.LogWarning("Product not found");
                return 0;
            }

            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                _logger.LogWarning("Product name is required");
                return 0;
            }

            if (dto.Name.Length < 3 || dto.Name.Length > 50)
            {
                _logger.LogWarning("Product name must be between 3 and 50 characters");
                return 0;
            }

            if (dto.Price <= 0)
            {
                _logger.LogWarning("Price must be greater than 0");
                return 0;
            }

            if (dto.QuantityInStock < 0)
            {
                _logger.LogWarning("Quantity in stock cannot be negative");
                return 0;
            }

            var categoryExists = await _context.Categories.AnyAsync(x => x.Id == dto.CategoryId);

            if (!categoryExists)
            {
                _logger.LogWarning("Category not found");
                return 0;
            }

            var supplierExists = await _context.Suppliers.AnyAsync(x => x.Id == dto.SupplierId);

            if (!supplierExists)
            {
                _logger.LogWarning("Supplier not found");
                return 0;
            }

            product.Name = dto.Name;
            product.Price = dto.Price;
            product.QuantityInStock = dto.QuantityInStock;
            product.CategoryId = dto.CategoryId;
            product.SupplierId = dto.SupplierId;

            await _context.SaveChangesAsync();
            return 1;
        }

        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while updating product");
            return 0;
        }
    }

    public async Task<int> DeleteProductAsync(int id)
    {
        try
        {
            if (id <= 0)
            {
                _logger.LogWarning("Invalid product id");
                return 0;
            }

            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                _logger.LogWarning("Product not found");
                return 0;
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return 1;
        }

        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while deleting product");
            return 0;
        }
    }

    public async Task<GetProductDto?> GetByIdAsync(int id)
    {
        try
        {
            if (id <= 0)
            {
                _logger.LogWarning("Invalid product id");
                return null;
            }

            return await _context.Products.AsNoTracking().Where(p => p.Id == id)
            .Select(p => new GetProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                QuantityInStock = p.QuantityInStock,
                CategoryId = p.CategoryId,
                SupplierId = p.SupplierId
            }).FirstOrDefaultAsync();
        }

        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while getting product by id");
            return null;
        }
    }
}