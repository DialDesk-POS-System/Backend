using DialDesk.Server.Data;
using DialDesk.Server.Interfaces;
using DialDesk.Server.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DialDesk.Server.Services
{
    public class InventoryLogService : IInventoryLogService
    {
        public readonly AppDbContext _context;
        public readonly Logger<ReturnService> _logger;

        public InventoryLogService(AppDbContext context, Logger<ReturnService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<InventoryLog>> GetAllAsync()
        {
            try
            {
                return await _context.InventoryLogs
                     .Include(x => x.Watch)
                    .ToListAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "error in InventoryLog when getting GetAllAsync ");
                throw;
            }
        }

        public async Task<InventoryLog> CreateLogAsync(InventoryLog log) 
        {
            try
            {
                _context.InventoryLogs.Add(log);
                await _context.SaveChangesAsync();
                return log;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "error in InventoryLog when CreateLogAsync");
                throw;
            }

        }

        public async Task<InventoryLog?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.InventoryLogs
                    .Include(x => x.Watch)
                    .FirstOrDefaultAsync(b => b.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "error in InventoryLog when GetByIdAsync");
                throw;
            }

        }

        public async Task<List<InventoryLog>> GetByChangeTypeAsync(ChangeType changeType)
        {
            try
            {
                return await _context.InventoryLogs
                    .Where(x => x.ChangeType == changeType)
                    .Include(x => x.Watch)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "error in InventoryLog when GetByChangeTypeAsync");
                throw;
            }

        }

        public async Task<List<InventoryLog>> GetByWatchIdAsync(string watchId)
        {
            try
            {
                return await _context.InventoryLogs
                    .Where(x => x.WatchId == watchId)
                    .Include(x => x.Watch)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "error in InventoryLog when GetByWatchIdAsync");
                throw;
            }
        }


        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var existLog = await _context.InventoryLogs.FindAsync(id);

                if (existLog == null)
                {
                    _logger.LogError("Id not found to delete");
                    return false;
                }

                 _context.InventoryLogs.Remove(existLog);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "error in InventoryLog when DeleteAsync ");
                throw;
            }
        }
    }
}
