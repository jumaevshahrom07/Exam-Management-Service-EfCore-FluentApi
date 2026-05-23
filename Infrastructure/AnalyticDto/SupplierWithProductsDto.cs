namespace Infrastructure.AnalyticDto;

public class SupplierWithProductsDto
{
    public int SupplierId { get; set; }
    public string SupplierName { get; set; } = string.Empty;

    public List<string> Products { get; set; } = [];
}