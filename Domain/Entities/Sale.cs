using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Sale
{
    [Key]
    public int Id { get; set; }
    public int ProductId { get; set; }

    [Range(0, int.MaxValue)]
    public int QuantitySold { get; set; }
    public DateTime SaleDate { get; set; } = DateTime.UtcNow;

    // navigation properties
    public Product Product { get; set; } = null!;
}