using DialDesk.Server.DTOs;
using DialDesk.Server.DTOs.Warranty;
using DialDesk.Server.Models;
using DialDesk.Server.Pagination;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DialDesk.Server.Interfaces
{
    public interface IWarrantyService
    {
        Task<Warranty> CreateWarrantyAsync(Warranty warranty);
        Task<Warranty?> GetByIdAsync(int id);
        Task<Warranty?> GetBySaleItemIdAsync(int saleItemId);
        //Task<Warranty?> GetByInvoiceAsync(int saleItemId);
        Task<Warranty?> GetByWatchIdAsync(Guid watchId);
        Task ClaimWarrantyAsync(int warrantyId, DateTime claimDate);
        Task<PagedResult<WarrantyOutDto>> GetPaginatedAsync(int page, int pageSize);
        Task<WarrantyDashboardDto> GetDashboardDataAsync();
        Task<bool> DeleteWarrentyAsync(int warrantyId);
    }
}
