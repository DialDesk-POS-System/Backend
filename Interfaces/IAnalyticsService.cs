namespace DialDesk.Server.Interfaces;

public interface IAnalyticsService
{
    Task<int> GetTotalUnitsAsync();
    Task<int> GetTotalModelsAsync();
    Task<int> GetLowStockCountAsync(int threshold = 5);
    Task<decimal> GetTotalStockValueAsync();
}
