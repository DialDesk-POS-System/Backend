using DialDesk.Server.Data;
using DialDesk.Server.DTOs.Watch;
using DialDesk.Server.Interfaces;
using DialDesk.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace DialDesk.Server.Services
{
    public class WatchService : IWatchService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<WatchService> _logger;

        private IQueryable<Watch> WatchesWithDetailsQuery => _context.Watches
            .Include(w => w.Model)
            .ThenInclude(m => m.Brand)
            .Include(w => w.Warranty);

        public WatchService(AppDbContext context, ILogger<WatchService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Watch>> GetAllWatchesAsync()
        {
            try
            {
                return await WatchesWithDetailsQuery.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching watches");
                throw;
            }
        }

        public async Task<Watch?> GetWatchByIdAsync(Guid id)
        {
            try
            {
                return await WatchesWithDetailsQuery.FirstOrDefaultAsync(w => w.Id == id);
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
                return await WatchesWithDetailsQuery
                    .Where(w => w.ModelId == modelId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching watches by model");
                throw;
            }
        }

        public async Task<List<Watch>> GetWatchesByBrandAsync(int brandId)
        {
            try
            {
                return await WatchesWithDetailsQuery
                    .Where(w => w.Model.BrandId == brandId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching watches by brand");
                throw;
            }
        }

        public async Task<Watch?> GetWatchBySerialNumberAsync(string serialNumber)
        {
            try
            {
                return await WatchesWithDetailsQuery
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
                watch.Status = Status.Available;
                if (string.IsNullOrWhiteSpace(watch.ImageryUrl))
                {
                    watch.ImageryUrl = (await _context.Models.FindAsync(watch.ModelId))?.ImageryUrl;
                }
                _context.Watches.Add(watch);
                await _context.SaveChangesAsync();
                return await WatchesWithDetailsQuery.FirstOrDefaultAsync(w => w.Id == watch.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating watch");
                throw;
            }
        }

        public async Task<Watch?> UpdateWatchAsync(Guid id, WatchUpdateDto watch)
        {
            try
            {
                var existingWatch = await _context.Watches.FindAsync(id);
                if (existingWatch == null)
                {
                    _logger.LogWarning("Watch with id {Id} not found for update", id);
                    return null;
                }

                _context.Entry(existingWatch).CurrentValues.SetValues(watch);
                await _context.SaveChangesAsync();
                return await WatchesWithDetailsQuery.FirstOrDefaultAsync(w => w.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating watch");
                throw;
            }
        }

        public async Task<bool> DeleteWatchAsync(Guid id)
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
                return await WatchesWithDetailsQuery
                    .Where(w => w.CostPrice >= minCostPrice && w.CostPrice <= maxCostPrice)
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
                return await WatchesWithDetailsQuery
                    .Where(w => w.SellingPrice >= minSellingPrice && w.SellingPrice <= maxSellingPrice)
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
                return await WatchesWithDetailsQuery
                    .Where(w => w.Status == status)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching watches by status");
                throw;
            }
        }

        public async Task<List<Watch>> SearchWatchesAsync(WatchSearchDto filter)
        {
            try
            {
                var query = WatchesWithDetailsQuery.AsQueryable();

                if (!string.IsNullOrEmpty(filter.ModelName))
                {
                    query = query.Where(w => w.Model.ModelName.Contains(filter.ModelName));
                }

                if (!string.IsNullOrWhiteSpace(filter.ModelNo))
                {
                    query = query.Where(w => w.Model.ModelNo.Contains(filter.ModelNo));
                }

                if (!string.IsNullOrWhiteSpace(filter.BrandName))
                {
                    query = query.Where(w => w.Model.Brand.Name.Contains(filter.BrandName));
                }

                if (filter.Category.HasValue)
                {
                    query = query.Where(w => w.Model.Category == filter.Category.Value);
                }

                if (!string.IsNullOrWhiteSpace(filter.SerialNo))
                {
                    query = query.Where(w => w.SerialNo.Contains(filter.SerialNo));
                }

                if (!string.IsNullOrEmpty(filter.Color))
                {
                    query = query.Where(w => w.Color == filter.Color);
                }
                if (!string.IsNullOrEmpty(filter.StrapMaterial))
                {
                    query = query.Where(w => w.StrapMaterial == filter.StrapMaterial);
                }

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching watches");
                throw;
            }
        }
    }
}
