using System.ComponentModel.DataAnnotations;

namespace Infrastructure.DTOs.ProductDto;

public class CreateProductDto
{
    [Required(ErrorMessage = "Product name is required")]
    [MinLength(3, ErrorMessage = "Product name must be minimum 3 characters")]
    [MaxLength(50, ErrorMessage = "Product name cannot be more than 50 characters")]
    public string Name { get; set; } = string.Empty;

    [Range(0.01, 999999, ErrorMessage = "Price must be greater than 0")]
    public decimal Price { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Stock cannot be negative")]
    public int QuantityInStock { get; set; }

    [Required(ErrorMessage = "CategoryId is required")]
    public int CategoryId { get; set; }

    [Required(ErrorMessage = "SupplierId is required")]
    public int SupplierId { get; set; }
}