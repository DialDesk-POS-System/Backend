using DialDesk.Server.Models;

namespace DialDesk.Server.Interfaces;

public interface IAnalyticsService
{
    Task<int> GetTotalUnitsAsync();
    Task<int> GetTotalModelsAsync();
    Task<decimal> GetTotalStockValueAsync();
    Task<decimal> GetTodayRevenue(DateTime date);
    Task<List<Model>> GetLowStockModels();
}
