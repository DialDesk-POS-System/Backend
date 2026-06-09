using DialDesk.Server.Models;

namespace DialDesk.Server.Interfaces
{
    public interface IEmailService
    {
        Task SendInvoiceEmailAsync(string toEmail, string customerName, string invoiceNo, decimal totalAmount, List<SaleItem> items);
    }
}
