using Infrastructure.AnalyticDto;

namespace Infrastructure.Interfaces;

public interface IAnalyticService
{
    Task<List<CategoryWithProductsDto>> GetCategoriesWithProductsAsync();
    Task<List<LowStockDto>> GetLowStockProductsAsync();
    Task<ProductStatisticsDto> GetProductStatisticsAsync();
    Task<List<SalesByDateDto>> GetSalesByDateAsync(DateTime from, DateTime to);
    Task<List<TopProductsDto>> GetTopProductsAsync();
    Task<List<DailyRevenueDto>> GetDailyRevenueAsync();
    Task<List<StockHistoryDto>> GetStockHistoryAsync(int productId);
    Task<ProductDetailsDto?> GetProductDetailsAsync(int id);
    Task<List<SupplierWithProductsDto>> GetSuppliersWithProductsAsync();
    Task<DashboardDto> GetDashboardStatisticsAsync();
}