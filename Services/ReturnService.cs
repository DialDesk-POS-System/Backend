using DialDesk.Server.Data;
using DialDesk.Server.Interfaces;
using DialDesk.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace DialDesk.Server.Services
{
    public class ReturnService : IReturnService
    {
        public readonly AppDbContext _context;
        public readonly ILogger<ReturnService> _logger;

        public ReturnService(AppDbContext context, Logger<ReturnService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Return>> GetAllAsync()
        {
            try
            {
                return await _context.Returns
                    .Include(x=>x.OriginalSaleItem)
                    .ToListAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "error in ReturnService when getting GetAllAsync ");
                throw;
            }

        }
        public async Task<Return> CreateReturnAsync(Return returnRequest)
        {
            try
            {
                returnRequest.ReturnDate = DateTime.UtcNow;
                _context.Returns.Add(returnRequest);
                await _context.SaveChangesAsync();
                return returnRequest;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "error in ReturnService when  CreateReturnAsync ");
                throw;
            }
        }
        public async Task<Return?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Returns
                    .Include(x => x.NewSaleItem)
                    .Include(x=>x.OriginalSaleItem)
                    .FirstOrDefaultAsync(w => w.Id == id);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "error in ReturnService when getting GetByIdAsync ");
                throw;
            }
        }
        public async Task<Return?> GetBySaleItemAsync(int saleItemId)
        {
            try
            {
                return await _context.Returns
                     .Include(r => r.OriginalSaleItem)
                        .Include(r => r.NewSaleItem)
                        .FirstOrDefaultAsync(r => r.OriginalSaleItemId == saleItemId);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "error in ReturnService when getting GetBySaleItemAsync ");
                throw;
            }
        }
        public async Task<bool> UpdateAsync(int id, Return returnItem)
        {
            try
            {
                var existingReturn = await _context.Returns.FindAsync(id);
                if (existingReturn == null)
                {
                    _logger.LogWarning("return with id  not found for update");
                    return false;
                }
                existingReturn.ReturnDate = returnItem.ReturnDate;
                existingReturn.OriginalSaleItemId = returnItem.OriginalSaleItemId;
                existingReturn.NewSaleItemId = returnItem.NewSaleItemId;
                existingReturn.RefundAmount = returnItem.RefundAmount;

                await _context.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "error in ReturnService when UpdateAsync");
                throw;
            }
        }
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var existReturn = await _context.Returns.FindAsync(id);

                if(existReturn == null)
                {
                    _logger.LogError("Id not found to delete");
                    return false;
                }

                _context.Returns.Remove(existReturn);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "error in ReturnService when DeleteAsync");
                throw;
            }
        }


    }
}
