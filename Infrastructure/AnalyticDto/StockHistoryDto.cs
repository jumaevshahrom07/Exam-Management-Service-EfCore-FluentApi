namespace Infrastructure.AnalyticDto;

public class StockHistoryDto
{
    public DateTime AdjustmentDate { get; set; }
    public int Amount { get; set; }
    public string Reason { get; set; } = string.Empty;
}