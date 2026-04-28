using System.ComponentModel.DataAnnotations;

namespace DialDesk.Server.Models
{
    public class Sale
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string InvoiceNo { get; set; }

        [Required]
        public DateTime SaleDate { get; set; }

        [Required]
        public decimal SubTotal { get; set; }

        public decimal? DiscountAmount { get; set; }

        public decimal? TaxAmount { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

        public string? InvoicePdfUrl { get; set; }

        [Required]
        public PaymentMethod PaymentMethod { get; set; }

        public string? Notes { get; set; }

        public string? CustomerName { get; set; }

        [EmailAddress]
        public string? CustomerEmail { get; set; }
        public string? CustomerPhone { get; set; }

        public List<SaleItem> SaleItems { get; set; } = [];
    }
}
