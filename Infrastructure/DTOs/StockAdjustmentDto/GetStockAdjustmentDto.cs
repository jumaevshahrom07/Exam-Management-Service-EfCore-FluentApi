namespace Infrastructure.DTOs.StockAdjustmentDto;

public class GetStockAdjustmentDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int AdjustmentAmount { get; set; }
    public string Reason { get; set; } = string.Empty;
    public DateTime AdjustmentDate { get; set; }
}