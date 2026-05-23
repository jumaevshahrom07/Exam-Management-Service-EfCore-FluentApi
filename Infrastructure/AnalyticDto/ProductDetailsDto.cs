namespace Infrastructure.AnalyticDto;

public class ProductDetailsDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int QuantityInStock { get; set; }

    public string Category { get; set; } = string.Empty;
    public string Supplier { get; set; } = string.Empty;

    public List<SaleMiniDto> Sales { get; set; } = [];
    public List<StockMiniDto> Adjustments { get; set; } = [];
}

public class SaleMiniDto
{
    public int Quantity { get; set; }
    public DateTime Date { get; set; }
}

public class StockMiniDto
{
    public int Amount { get; set; }
    public DateTime Date { get; set; }
    public string Reason { get; set; } = string.Empty;
}