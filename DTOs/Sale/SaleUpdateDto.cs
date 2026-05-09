using DialDesk.Server.Models;
using System.ComponentModel.DataAnnotations;

namespace DialDesk.Server.DTOs.Sale
{
    public class SaleUpdateDto
    {
        public decimal? DiscountAmount { get; set; }

        public decimal? TaxAmount { get; set; }

        [Required]
        public PaymentMethod PaymentMethod { get; set; }

        public string? Notes { get; set; }

        public string? CustomerName { get; set; }

        [EmailAddress]
        public string? CustomerEmail { get; set; }

        public string? CustomerPhone { get; set; }
    }
}
