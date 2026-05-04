using DialDesk.Server.Data;
using DialDesk.Server.DTOs;
using DialDesk.Server.Interfaces;
using DialDesk.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace DialDesk.Server.Services
{
    public class SaleService : ISaleService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<SaleService> _logger;

        public SaleService(AppDbContext context, ILogger<SaleService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Sale>> GetAllSalesAsync()
        {
            try
            {
                return await _context.Sales
                    .Include(s => s.PaymentMethod)
                    .Include(s => s.SaleItems)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching sales");
                throw;
            }
        }

        public async Task<Sale?> GetSaleByIdAsync(int id)
        {
            try
            {
                return await _context.Sales
                     .Include(s => s.PaymentMethod)
                     .Include(s => s.SaleItems)
                     .FirstOrDefaultAsync(s => s.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching sale by id");
                throw;
            }
        }

        public async Task<Sale?> GetSaleByInvoiceNo(string invoiceNo)
        {
            try
            {
                return await _context.Sales
                    .Include(s => s.PaymentMethod)
                    .Include(s => s.SaleItems)
                    .FirstOrDefaultAsync(s => s.InvoiceNo == invoiceNo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching sale by invoice number");
                throw;
            }
        }

        public async Task<Sale?> CreateSaleAsync(Sale sale)
        {
            try
            {
                _context.Sales.Add(sale);
                await _context.SaveChangesAsync();
                return sale;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating sale");
                throw;
            }
        }

        public async Task<Sale?> UpdateSaleAsync(int id, Sale sale)
        {
            try
            {
                var existingSale = await _context.Sales.FindAsync(id);
                if (existingSale == null)
                {
                    _logger.LogWarning("Sale with id {Id} not found for update", id);
                    return null;
                }
                _context.Entry(existingSale).CurrentValues.SetValues(sale);
                await _context.SaveChangesAsync();
                return existingSale;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating sale");
                throw;
            }
        }

        public async Task<bool> DeleteSaleAsync(int id)
        {
            try
            {
                var existingSale = await _context.Sales.FindAsync(id);
                if (existingSale == null)
                {
                    _logger.LogWarning("Sale with id {Id} not found for deletion", id);
                    return false;
                }
                _context.Sales.Remove(existingSale);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting sale");
                throw;
            }
        }

        public async Task<List<Sale>> SearchSalesAsync(SaleSearchDto filter)
        {
            var query = _context.Sales
                .Include(s => s.PaymentMethod)
                .Include(s => s.SaleItems)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.InvoiceNo))
                query = query.Where(s => s.InvoiceNo.Contains(filter.InvoiceNo));
            
            if (filter.SaleDateFrom.HasValue)
                query = query.Where(s => s.SaleDate >= filter.SaleDateFrom.Value);

            if (filter.SaleDateTo.HasValue)
                query = query.Where(s => s.SaleDate <= filter.SaleDateTo.Value);

            if (filter.SubTotal.HasValue)
                query = query.Where(s => s.SubTotal == filter.SubTotal.Value);

            if (filter.DiscountAmount.HasValue)
                query = query.Where(s => s.DiscountAmount == filter.DiscountAmount.Value);

            if (filter.TaxAmount.HasValue)
                query = query.Where(s => s.TaxAmount == filter.TaxAmount.Value);

            if (filter.TotalAmount.HasValue)
                query = query.Where(s => s.TotalAmount == filter.TotalAmount.Value);
            
            if (filter.PaymentMethod.HasValue)
                query = query.Where(s => s.PaymentMethod == filter.PaymentMethod.Value);

            if (!string.IsNullOrWhiteSpace(filter.CustomerName))
                query = query.Where(s => s.CustomerName != null && s.CustomerName.Contains(filter.CustomerName));

            if (!string.IsNullOrWhiteSpace(filter.CustomerEmail))
                query = query.Where(s => s.CustomerEmail != null &&
                                         s.CustomerEmail.Contains(filter.CustomerEmail));

            if (!string.IsNullOrWhiteSpace(filter.CustomerPhone))
                query = query.Where(s => s.CustomerPhone != null &&
                                         s.CustomerPhone.Contains(filter.CustomerPhone));

            if (!string.IsNullOrWhiteSpace(filter.Notes))
                query = query.Where(s => s.Notes != null &&
                                         s.Notes.Contains(filter.Notes));

            return await query.ToListAsync();
        }
    }
}
