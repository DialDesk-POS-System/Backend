using AutoMapper;
using DialDesk.Server.Data;
using DialDesk.Server.DTOs.BulkImport;
using DialDesk.Server.DTOs.Watch;
using DialDesk.Server.Interfaces;
using DialDesk.Server.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DialDesk.Server.Services
{
    public class BulkImportService : IBulkImportService
    {
        public readonly AppDbContext _context;
        public readonly ILogger<ReturnService> _logger;
        private readonly IMapper _mapper;

        public BulkImportService(AppDbContext context, IMapper mapper, ILogger<ReturnService> logger)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<BulkImport>> GetAllAsync()
        {
            try
            {
                return await _context.BulkImports
                    .ToListAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "error in BulkImports when getting GetAllAsync ");
                throw;
            }
        }


        public async Task<BulkImport> CreateImportAsync(BulkImportCreateDto dto)
        {
            try
            {
                var bulkImport = _mapper.Map<BulkImport>(dto);
                
                _context.BulkImports.Add(bulkImport);
                await _context.SaveChangesAsync();

                var watches = new List<Watch>();

                foreach (var w in dto.Watches)
                {
                    for (int i = 0; i < w.Quantity; i++)
                    {
                        var watch = _mapper.Map<Watch>(w);

                        watch.ImportId = bulkImport.Id;
                        watch.Status = Status.Available;

                        watch.RecievedAt =
                            DateTime.UtcNow;

                        watch.UpdatedAt =
                            DateTime.UtcNow;

                        watches.Add(watch);
                    }
                }
                    _context.Watches.AddRange(watches);
                    await _context.SaveChangesAsync();

                    return bulkImport;

                }
            catch (Exception ex)
            {
                _logger.LogError(ex, "error in BulkImports when CreateImportAsync");
                throw;
            }

        }

        public async Task<BulkImport?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.BulkImports
                    .Include(b => b.Watches)
                    .FirstOrDefaultAsync(b => b.Id == id);            
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "error in BulkImports when GetByIdAsync");
                throw;
            }


        }

        public async Task<bool> UpdateAsync(int id, BulkImport bulkImport)
        {

            try
            {
                var existBulkImport = await _context.BulkImports.FindAsync(id);
                if (existBulkImport == null)
                {
                    _logger.LogWarning("bulkImport with id not found for update");
                    return false;
                }

                existBulkImport.ImportDate = bulkImport.ImportDate;
                existBulkImport.TotalItems = bulkImport.TotalItems;
                existBulkImport.Supplier = bulkImport.Supplier;

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "error in BulkImports when UpdateAsync");
                throw;
            }

            
        }


        public async Task<IEnumerable<BulkImport>> GetBySupplierAsync(string supplier)
        {
            try
            {
               return await _context.BulkImports
                    .Where(b => b.Supplier == supplier)
                    .Include(b => b.Watches)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "error in BulkImports when GetBySupplierAsync");
                throw;
            }
        }

        public async Task<IEnumerable<BulkImport>> GetByDateRangeAsync(DateTime from, DateTime to)
        {

            try
            {
                return await _context.BulkImports
                     .Where(b => b.ImportDate >= from && b.ImportDate <= to)
                     .Include(b => b.Watches)
                     .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "error in BulkImports when GetByDateRangeAsync");
                throw;
            }


        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var existBulk = await _context.BulkImports.FindAsync(id);

                if (existBulk == null)
                {
                    _logger.LogError("Id not found to delete");
                    return false;
                }

                _context.BulkImports.Remove(existBulk);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "error in BulkImports when DeleteAsync ");
                throw;
            }
        }
    }
}
