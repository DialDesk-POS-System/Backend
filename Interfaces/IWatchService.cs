using DialDesk.Server.DTOs;
using DialDesk.Server.DTOs.Watch;
using DialDesk.Server.Models;

namespace DialDesk.Server.Interfaces
{
    public interface IWatchService
    {
        Task<List<Watch>> GetAllWatchesAsync();
        Task<Watch?> GetWatchByIdAsync(string id);
        Task<List<Watch>> GetWatchesByModelAsync(int modelId);
        Task<List<Watch>> GetWatchesByBrandAsync(int brandId);
        Task<Watch?> GetWatchBySerialNumberAsync(string serialNumber);
        Task<Watch?> CreateWatchAsync(Watch watch);
        Task<Watch?> UpdateWatchAsync(string id, WatchUpdateDto watch);
        Task<bool> DeleteWatchAsync(string id);
        Task<List<Watch>> GetWatchesBetweenCostPriceAsync(decimal minCostPrice, decimal maxCostPrice);
        Task<List<Watch>> GetWatchesBetweenSellingPriceAsync(decimal minSellingPrice, decimal maxSellingPrice);
        Task<List<Watch>> GetWatchesByStatusAsync(Status status);
    }
}
