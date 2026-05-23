using System.ComponentModel.DataAnnotations;

namespace Infrastructure.DTOs.SaleDto;

public class UpdateSaleDto
{
    [Required]
    public int ProductId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be minimum 1")]
    public int QuantitySold { get; set; }

    public DateTime SaleDate { get; set; }
}