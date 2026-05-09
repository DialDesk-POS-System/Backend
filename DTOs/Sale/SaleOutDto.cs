using DialDesk.Server.Models;

namespace DialDesk.Server.DTOs.Sale
{
    public class SaleOutDto
    {
        public int Id { get; set; }

        public string InvoiceNo { get; set; }

        public DateTime SaleDate { get; set; }

        public decimal SubTotal { get; set; }

        public decimal? DiscountAmount { get; set; }

        public decimal? TaxAmount { get; set; }

        public decimal TotalAmount { get; set; }

        public string? InvoicePdfUrl { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public string? Notes { get; set; }

        public string? CustomerName { get; set; }

        public string? CustomerEmail { get; set; }

        public string? CustomerPhone { get; set; }

        public List<SaleItemOutDto> SaleItems { get; set; } = [];
    }
}
