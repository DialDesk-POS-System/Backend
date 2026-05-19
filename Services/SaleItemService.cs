using DialDesk.Server.Data;
using DialDesk.Server.Interfaces;
using DialDesk.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace DialDesk.Server.Services
{
    public class SaleItemService : ISaleItemService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<SaleItemService> _logger;

        public SaleItemService(AppDbContext context, ILogger<SaleItemService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<SaleItem>> GetAllAsync()
        {
            try
            {
                return await _context.SaleItems
                    .Include(x => x.Sale)
                    .Include(x => x.Watch)
                    .Include(x => x.Warranty)
                    .Include(x => x.ReturnAsOriginal)
                    .Include(x => x.ReturnAsNew)
                    .ToListAsync();

            }
            catch (Exception ex) {

                _logger.LogError(ex, "error getting SaleItem GetAllAsync");
                throw;
            }
        }

        public async Task<SaleItem?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.SaleItems
                     .Include(x => x.Sale)
                    .Include(x => x.Watch)
                    .Include(x => x.Warranty)
                    .Include(x => x.ReturnAsOriginal)
                    .Include(x => x.ReturnAsNew)
                    .FirstOrDefaultAsync(x => x.Id == id);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching SaleItem ById");
                throw;
            }
        }

        public async Task<SaleItem> CreateAsync(SaleItem saleItem)
        {
            try
            {
                _context.SaleItems.Add(saleItem);
                await _context.SaveChangesAsync();
                return saleItem;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error CreateAsync SaleItem");
                throw;
            }
        }

        public async Task<SaleItem?> UpdateAsync(int id, SaleItem saleItem){

            try
            {
                var existingSale = await _context.SaleItems.FindAsync(id);

                if (existingSale == null)
                {
                    _logger.LogWarning("Watch with id {Id} not found for update", id);
                    return null;
                }

                existingSale.UnitPrice = saleItem.UnitPrice;
                existingSale.CostPrice = saleItem.CostPrice;
                existingSale.DiscountAmount = saleItem.DiscountAmount;
                existingSale.LineTotal = saleItem.LineTotal;
                existingSale.SaleId = saleItem.SaleId;
                existingSale.WatchId = saleItem.WatchId;

                await _context.SaveChangesAsync();

                return existingSale; 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error UpdateAsync SaleItem");
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
            var existingSale = await _context.SaleItems.FindAsync(id);

            if (existingSale == null)
            {
                _logger.LogWarning("SaleItems with id not found for Delete");
                return false;
            }
            _context.SaleItems.Remove(existingSale);
            await _context.SaveChangesAsync();
            return true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting SaleItems");
                throw;
            }
        }

        public async Task<IEnumerable<SaleItem>> GetBySaleIdAsync(int saleId)
        {
            try
            {
                return await _context.SaleItems
                    .Where(x => x.SaleId == saleId)
                    .Include(x => x.Watch)      
                    .Include(x => x.Warranty) 
                    .ToListAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error GetBySaleIdAsync SaleItems");
                throw;
            }
        }

    }
}
