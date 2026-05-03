using DialDesk.Server.Models;

namespace DialDesk.Server.Interfaces
{
    public interface IInventoryLogService
    {
        Task<InventoryLog> CreateLogAsync(InventoryLog log);
        Task<InventoryLog?> GetByIdAsync(int id);
        Task<List<InventoryLog>> GetAllAsync();
        Task<List<InventoryLog>> GetByWatchIdAsync(string watchId);
        Task<List<InventoryLog>> GetByChangeTypeAsync(ChangeType changeType);
    }
}
