namespace Infrastructure.DTOs.SaleDto;

public class GetSaleDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int QuantitySold { get; set; }
    public DateTime SaleDate { get; set; } = DateTime.UtcNow;
}