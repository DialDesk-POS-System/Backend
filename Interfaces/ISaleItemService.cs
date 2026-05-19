using DialDesk.Server.Models;

namespace DialDesk.Server.Interfaces
{
    public interface ISaleItemService
    {
        Task<IEnumerable<SaleItem>> GetAllAsync();

        Task<SaleItem?> GetByIdAsync(int id);

        Task<SaleItem> CreateAsync(SaleItem saleItem);

        Task<SaleItem?> UpdateAsync(int id, SaleItem saleItem);

        Task<bool> DeleteAsync(int id);

        Task<IEnumerable<SaleItem>> GetBySaleIdAsync(int saleId);


    }
}
