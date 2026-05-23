using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class StockAdjustment
{
    [Key]
    public int Id { get; set; }
    public int ProductId { get; set; }

    [Range(0, int.MaxValue)]
    public int AdjustmentAmount { get; set; }

    [Required, MaxLength(100)]
    public string Reason { get; set; } = string.Empty;
    public DateTime AdjustmentDate { get; set; } = DateTime.UtcNow;

    // navigation propertiescle
    public Product Product { get; set; } = null!;
}