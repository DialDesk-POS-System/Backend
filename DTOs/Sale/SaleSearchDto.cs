using DialDesk.Server.Models;

namespace DialDesk.Server.DTOs
{
    public class SaleSearchDto
    {
        public string? InvoiceNo { get; set; }
        public DateTime? SaleDateFrom { get; set; }
        public DateTime? SaleDateTo { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? TaxAmount { get; set; }
        public decimal? TotalAmount { get; set; }
        public PaymentMethod? PaymentMethod { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerEmail { get; set; }
        public string? CustomerPhone { get; set; }
        public string? Notes { get; set; }
    }
}
