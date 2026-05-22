using DialDesk.Server.Data;
using DialDesk.Server.DTOs;
using DialDesk.Server.DTOs.Warranty;
using DialDesk.Server.Interfaces;
using DialDesk.Server.Models;
using DialDesk.Server.Pagination;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DialDesk.Server.Services
{
    public class WarrantyService : IWarrantyService
    {
        private readonly AppDbContext _db;
        private readonly ILogger<WarrantyService> _logger;

        public WarrantyService(AppDbContext db, ILogger<WarrantyService> logger)
        {
            _db = db;
            _logger = logger;

        }

        public async Task<PagedResult<WarrantyOutDto>> GetPaginatedAsync(int page, int pageSize)
        {
            try
            {
                var totalCount = await _db.Warranties.CountAsync();

                var warrenties = await _db.Warranties
                     .Include(w => w.SaleItem)
                        .ThenInclude(si => si.Sale)
                    .OrderByDescending(w => w.Id)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var items = warrenties.Select(w => new WarrantyOutDto
                {
                    Id = w.Id,
                    SaleItemId = w.SaleItemId,
                    InvoiceNo = w.SaleItem.Sale.InvoiceNo,
                    ClaimDate = w.ClaimDate,
                    EndDate = w.EndDate,
                    Status =
                        w.IsClaimed ? "Claimed" :
                        w.EndDate < DateTime.UtcNow ? "Expired" :
                        w.EndDate <= DateTime.UtcNow.AddDays(30) ? "Expiring" : "Active"


                }).ToList();

                return new PagedResult<WarrantyOutDto>
                {
                    Items = items,
                    TotalCount = totalCount,
                    PageNumber = page,
                    PageSize = pageSize,
                    
                };

            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error Creating WarrantyAsync");
                throw;
            }


        }

        public async Task<WarrantyDashboardDto> GetDashboardDataAsync()
        {
            var now = DateTime.UtcNow;

            try
            {
                var activeCount = await _db.Warranties.CountAsync(
                   W => W.EndDate > now && !W.IsClaimed
                       );
                var expiring30Count = await _db.Warranties
                    .CountAsync(w =>
                        w.EndDate > now &&
                        w.EndDate <= now.AddDays(30) &&
                        !w.IsClaimed);
                var expiredCount = await _db.Warranties
                       .CountAsync(w =>
                           w.EndDate < now);
                var claimedCount = await _db.Warranties
                      .CountAsync(w =>
                          w.IsClaimed);

                return new WarrantyDashboardDto
                {
                    ActiveCount = activeCount,
                    Expiring30DaysCount = expiring30Count,
                    ExpiredCount = expiredCount,
                    ClaimedCount = claimedCount,

                };

            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error Creating WarrantyAsync");
                throw;
            }

        }

        public async Task<Warranty> CreateWarrantyAsync(Warranty warranty)
        {
            try
            {
            _db.Warranties.Add(warranty);
            await _db.SaveChangesAsync();
            return warranty;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Creating WarrantyAsync");
                throw;
            }

        }

        public async Task<Warranty?> GetByIdAsync(int id)
        {
            try
            {
            return await _db.Warranties
                //.Include(w => w.Watch)
                .Include(w => w.SaleItem)
                .FirstOrDefaultAsync(w => w.Id == id);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching Warranty By Id");
                throw;
            }

        }

        public async Task<Warranty?> GetBySaleItemIdAsync(int saleItemId)
        {
            try
            {
            return await _db.Warranties
                //.Include(w => w.Watch)
                .Include(w => w.SaleItem)
                .FirstOrDefaultAsync(w => w.SaleItemId == saleItemId);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching Warranty BySaleItemId");
                throw;
            }
        }

        public async Task<Warranty?> GetByWatchIdAsync(Guid watchId)
        {
            try
            {
            return await _db.Warranties
                //.Include(w => w.Watch)
                .Include(w => w.SaleItem)
                .FirstOrDefaultAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching Warranty ByWatchId");
                 throw;

            }
        }

        public async Task ClaimWarrantyAsync(int warrantyId, DateTime claimDate)
        {
            try
            {
            var warranty = await _db.Warranties.FindAsync(warrantyId);

            if (warranty != null)
            {
                warranty.IsClaimed = true;
                warranty.ClaimDate = claimDate;
                await _db.SaveChangesAsync();
            }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error ClaimWarranty ");
                throw;
            }
        }

        public async Task<bool> DeleteWarrentyAsync(int warrantyId)
        {

            try
            {
                var existwarrenty = await _db.Warranties.FindAsync(warrantyId);
                if (existwarrenty == null)
                {
                    _logger.LogWarning("warrenty with id  not found for deletion");
                    return false;
                }
                _db.Warranties.Remove(existwarrenty);
                await _db.SaveChangesAsync();
                return true;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Error DeleteWarrentyAsync ");
                throw;
            }
        }


    }
}
