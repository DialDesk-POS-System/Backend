using DialDesk.Server.Models;

namespace DialDesk.Server.Interfaces
{
    public interface IWarrantyService
    {
        Task<Warranty> CreateWarrantyAsync(Warranty warranty);
        Task<Warranty?> GetByIdAsync(int id);
        Task<Warranty?> GetBySaleItemIdAsync(int saleItemId);
        Task<Warranty?> GetByWatchIdAsync(Guid watchId);
        Task ClaimWarrantyAsync(int warrantyId, DateTime claimDate);

        Task<bool> DeleteWarrentyAsync(int warrantyId);
    }
}
