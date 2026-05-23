using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Product
{
    [Key]
    public int Id { get; set; }

    [Required, MinLength(3), MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    [Range(0.01, 999999)]
    public decimal Price { get; set; }

    [Range(0, int.MaxValue)]
    public int QuantityInStock { get; set; }
    public int CategoryId { get; set; }
    public int SupplierId { get; set; }

    // navigation properties
    public Category Category { get; set; } = null!;
    public Supplier Supplier { get; set; } = null!;
    public List<Sale> Sales { get; set; } = [];
    public List<StockAdjustment> StockAdjustments { get; set; } = [];
}