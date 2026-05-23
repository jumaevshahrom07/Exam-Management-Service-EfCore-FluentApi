namespace Infrastructure.AnalyticDto;

public class LowStockDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int QuantityInStock { get; set; }
}