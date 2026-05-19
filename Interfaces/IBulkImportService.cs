using DialDesk.Server.DTOs.BulkImport;
using DialDesk.Server.Models;

namespace DialDesk.Server.Interfaces
{
    public interface IBulkImportService
    {
        Task<BulkImport> CreateImportAsync(BulkImportCreateDto dto);
        Task<BulkImport?> GetByIdAsync(int id);
        Task<List<BulkImport>> GetAllAsync();
        Task<bool> UpdateAsync(int id, BulkImport bulkImport);

        Task<bool> DeleteAsync(int id);

        Task<IEnumerable<BulkImport>> GetBySupplierAsync(string supplier);

        Task<IEnumerable<BulkImport>> GetByDateRangeAsync(DateTime from, DateTime to);
    }
}
