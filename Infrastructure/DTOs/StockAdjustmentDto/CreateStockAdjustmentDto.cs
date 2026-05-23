using System.ComponentModel.DataAnnotations;

namespace Infrastructure.DTOs.StockAdjustmentDto;

public class CreateStockAdjustmentDto
{
    [Required(ErrorMessage = "ProductId is required")]
    public int ProductId { get; set; }

    [Range(-100000, 100000, ErrorMessage = "Adjustment amount is invalid")]
    public int AdjustmentAmount { get; set; }

    [Required(ErrorMessage = "Reason is required")]
    [MaxLength(100, ErrorMessage = "Reason cannot be more than 100 characters")]
    public string Reason { get; set; } = string.Empty;

    [Required(ErrorMessage = "Adjustment date is required")]
    public DateTime AdjustmentDate { get; set; }
}