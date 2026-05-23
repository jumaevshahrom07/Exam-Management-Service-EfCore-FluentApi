namespace Infrastructure.AnalyticDto;

public class CategoryWithProductsDto
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;

    public List<ProductShortDto> Products { get; set; } = [];
}

public class ProductShortDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}