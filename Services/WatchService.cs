using DialDesk.Server.Data;
using DialDesk.Server.Interfaces;
using DialDesk.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace DialDesk.Server.Services
{
    public class WatchService : IWatchService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<WatchService> _logger;

        public WatchService(AppDbContext context, ILogger<WatchService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Watch>> GetAllWatchesAsync()
        {
            try
            {
                return await _context.Watches
                    .Include(w => w.Model)
                    .Include(w => w.Warranty)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching watches");
                throw;
            }
        }

        public async Task<Watch?> GetWatchByIdAsync(string id)
        {
            try
            {
                return await _context.Watches
                    .Include(w => w.Model)
                    .Include(w => w.Warranty)
                    .FirstOrDefaultAsync(w => w.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching watch by id");
                throw;
            }
        }

        public async Task<List<Watch>> GetWatchesByModelAsync(int modelId)
        {
            try
            {
                return await _context.Watches
                    .Where(w => w.ModelId == modelId)
                    .Include(w => w.Model)
                    .Include(w => w.Warranty)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching watches by model");
                throw;
            }
        }

        public async Task<Watch?> GetWatchBySerialNumberAsync(string serialNumber)
        {
            try
            {
                return await _context.Watches
                    .Include(w => w.Model)
                    .Include(w => w.Warranty)
                    .FirstOrDefaultAsync(w => w.SerialNo == serialNumber);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching watch by serial number");
                throw;
            }
        }

        public async Task<Watch?> CreateWatchAsync(Watch watch)
        {
            try
            {
                watch.RecievedAt = DateTime.UtcNow;
                watch.UpdatedAt = DateTime.UtcNow;
                _context.Watches.Add(watch);
                await _context.SaveChangesAsync();
                return watch;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating watch");
                throw;
            }
        }

        public async Task<Watch?> UpdateWatchAsync(string id, Watch watch)
        {
            try
            {
                var existingWatch = await _context.Watches.FindAsync(id);
                if (existingWatch == null)
                {
                    _logger.LogWarning("Watch with id {Id} not found for update", id);
                    return null;
                }

                existingWatch.ModelId = watch.ModelId;
                existingWatch.SerialNo = watch.SerialNo;
                existingWatch.Color = watch.Color;
                existingWatch.StrapMaterial = watch.StrapMaterial;
                existingWatch.WaterResistanceM = watch.WaterResistanceM;
                existingWatch.CostPrice = watch.CostPrice;
                existingWatch.SellingPrice = watch.SellingPrice;
                existingWatch.Status = watch.Status;
                existingWatch.ImageryUrl = watch.ImageryUrl;
                existingWatch.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();
                return existingWatch;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating watch");
                throw;
            }
        }

        public async Task<bool> DeleteWatchAsync(string id)
        {
            try
            {
                var watch = await _context.Watches.FindAsync(id);
                if (watch == null)
                {
                    _logger.LogWarning("Watch with id {Id} not found for deletion", id);
                    return false;
                }

                _context.Watches.Remove(watch);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting watch");
                throw;
            }
        }

        public async Task<List<Watch>> GetWatchesBetweenCostPriceAsync(decimal minCostPrice, decimal maxCostPrice)
        {
            try
            {
                return await _context.Watches
                    .Where(w => w.CostPrice >= minCostPrice && w.CostPrice <= maxCostPrice)
                    .Include(w => w.Model)
                    .Include(w => w.Warranty)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching watches between cost price");
                throw;
            }
        }

        public async Task<List<Watch>> GetWatchesBetweenSellingPriceAsync(decimal minSellingPrice, decimal maxSellingPrice)
        {
            try
            {
                return await _context.Watches
                    .Where(w => w.SellingPrice >= minSellingPrice && w.SellingPrice <= maxSellingPrice)
                    .Include(w => w.Model)
                    .Include(w => w.Warranty)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching watches between selling price");
                throw;
            }
        }

        public async Task<List<Watch>> GetWatchesByStatusAsync(Status status)
        {
            try
            {
                return await _context.Watches
                    .Where(w => w.Status == status)
                    .Include(w => w.Model)
                    .Include(w => w.Warranty)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching watches by status");
                throw;
            }
        }
    }
}
