using DialDesk.Server.Models;

namespace DialDesk.Server.Interfaces
{
    public interface ISaleInterface
    {
        Task<List<Sale>> GetAllSalesAsync();
        Task<Sale> GetSaleByIdAsync(int id);
        Task<Sale> GetSaleByInvoiceNo(string invoiceNo);
        Task<Sale> CreateSaleAsync(Sale sale);
        Task<Sale> UpdateSaleAsync(int id, Sale sale);
        Task<bool> DeleteSaleAsync(int id);
        Task<List<Sale>> SearchSalesAsync(Sale sale);
    }
}
