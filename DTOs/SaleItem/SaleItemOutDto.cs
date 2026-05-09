using DialDesk.Server.Models;

namespace DialDesk.Server.DTOs
{
    public class SaleItemOutDto
    {
        public int Id { get; set; }

        public int SaleId { get; set; }

        public string WatchId { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal CostPrice { get; set; }

        public decimal DiscountAmount { get; set; }

        public decimal LineTotal { get; set; }

        public DateTime CreatedAt { get; set; }

        public WarrantyOutDto? Warranty { get; set; }
    }
}
