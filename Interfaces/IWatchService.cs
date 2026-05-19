using DialDesk.Server.DTOs;
using DialDesk.Server.DTOs.Watch;
using DialDesk.Server.Models;
using DialDesk.Server.Pagination;

namespace DialDesk.Server.Interfaces
{
    public interface IWatchService
    {
        Task<PagedResult<Watch>> GetAllWatchesAsync(int pageNumber = 1, int pageSize = 10);
        Task<Watch?> GetWatchByIdAsync(Guid id);
        Task<List<Watch>> GetWatchesByModelAsync(int modelId);
        Task<List<Watch>> GetWatchesByBrandAsync(int brandId);
        Task<Watch?> GetWatchBySerialNumberAsync(string serialNumber);
        Task<Watch?> CreateWatchAsync(Watch watch);
        Task<Watch?> UpdateWatchAsync(Guid id, WatchUpdateDto watch);
        Task<bool> DeleteWatchAsync(Guid id);
        Task<List<Watch>> GetWatchesBetweenCostPriceAsync(decimal minCostPrice, decimal maxCostPrice);
        Task<List<Watch>> GetWatchesBetweenSellingPriceAsync(decimal minSellingPrice, decimal maxSellingPrice);
        Task<List<Watch>> GetWatchesByStatusAsync(Status status);
        Task<List<Watch>> SearchWatchesAsync(WatchSearchDto filter);
    }
}
