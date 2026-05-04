using DialDesk.Server.Data;
using DialDesk.Server.Interfaces;
using DialDesk.Server.Models;
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
                .Include(w => w.Watch)
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
                .Include(w => w.Watch)
                .Include(w => w.SaleItem)
                .FirstOrDefaultAsync(w => w.SaleItemId == saleItemId);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching Warranty BySaleItemId");
                throw;
            }
        }

        public async Task<Warranty?> GetByWatchIdAsync(string watchId)
        {
            try
            {
            return await _db.Warranties
                .Include(w => w.Watch)
                .Include(w => w.SaleItem)
                .FirstOrDefaultAsync(w => w.WatchId == watchId);

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
