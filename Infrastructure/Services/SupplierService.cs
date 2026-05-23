using Domain.Entities;
using Infrastructure.DTOs.SupplierDto;
using Infrastructure.Interfaces;
using Infrastructure.Persistence.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services;

public class SupplierService : ISupplierService
{
    private readonly AppDbContext _context;
    private readonly ILogger<SupplierService> _logger;


    public SupplierService(AppDbContext context, ILogger<SupplierService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<List<GetSupplierDto>> GetAllAsync()
    {
        try
        {
            return await _context.Suppliers.AsNoTracking()
                .Select(s => new GetSupplierDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Phone = s.Phone
                }).ToListAsync();
        }

        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while getting suppliers");
            return [];
        }
    }

    public async Task<int> CreateSupplierAsync(CreateSupplierDto dto)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                _logger.LogWarning("Supplier name is required");
                return 0;
            }

            if (dto.Name.Length < 3 || dto.Name.Length > 50)
            {
                _logger.LogWarning("Supplier name must be between 3 and 50 characters");
                return 0;
            }

            if (string.IsNullOrWhiteSpace(dto.Phone))
            {
                _logger.LogWarning("Phone number is required");
                return 0;
            }

            if (dto.Phone.Length < 9 || dto.Phone.Length > 13)
            {
                _logger.LogWarning("Phone number must be between 9 and 13 characters");
                return 0;
            }

            var supplier = new Supplier
            {
                Name = dto.Name,
                Phone = dto.Phone
            };

            await _context.Suppliers.AddAsync(supplier);
            return await _context.SaveChangesAsync();
        }

        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while creating supplier");
            return 0;
        }
    }

    public async Task<int> UpdateSupplierAsync(int id, UpdateSupplierDto dto)
    {
        try
        {
            if (id <= 0)
            {
                _logger.LogWarning("Invalid supplier id");
                return 0;
            }

            var supplier = await _context.Suppliers
                .FirstOrDefaultAsync(s => s.Id == id);

            if (supplier == null)
            {
                _logger.LogWarning("Supplier not found");
                return 0;
            }

            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                _logger.LogWarning("Supplier name is required");
                return 0;
            }

            if (dto.Name.Length < 3 || dto.Name.Length > 50)
            {
                _logger.LogWarning("Supplier name must be between 3 and 50 characters");
                return 0;
            }

            if (string.IsNullOrWhiteSpace(dto.Phone))
            {
                _logger.LogWarning("Phone number is required");
                return 0;
            }

            if (dto.Phone.Length < 9 || dto.Phone.Length > 20)
            {
                _logger.LogWarning("Phone number must be between 9 and 20 characters");
                return 0;
            }

            supplier.Name = dto.Name;
            supplier.Phone = dto.Phone;

            await _context.SaveChangesAsync();
            return 1;
        }

        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while updating supplier");
            return 0;
        }
    }

    public async Task<int> DeleteSupplierAsync(int id)
    {
        try
        {
            if (id <= 0)
            {
                _logger.LogWarning("Invalid supplier id");
                return 0;
            }

            var supplier = await _context.Suppliers.FindAsync(id);

            if (supplier == null)
            {
                _logger.LogWarning("Supplier not found");
                return 0;
            }

            _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync();
            return 1;
        }

        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while deleting supplier");
            return 0;
        }
    }

    public async Task<GetSupplierDto?> GetByIdAsync(int id)
    {
        try
        {
            if (id <= 0)
            {
                _logger.LogWarning("Invalid supplier id");
                return null;
            }

            return await _context.Suppliers.AsNoTracking()
                .Select(s => new GetSupplierDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Phone = s.Phone
                }).FirstOrDefaultAsync(s => s.Id == id);
        }

        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while getting supplier by id");
            return null;
        }
    }
}