using DialDesk.Server.Models;
using System.ComponentModel.DataAnnotations;

namespace DialDesk.Server.DTOs
{
    public class SaleItemCreateDto
    {
        public string WatchId { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal DiscountAmount { get; set; }

        public decimal LineTotal { get; set; }
    }
}
