using DialDesk.Server.Models;
using System.ComponentModel.DataAnnotations;

namespace DialDesk.Server.DTOs.Watch
{
    public class WatchOutDto
    {
        public string Id { get; set; }
        public int ModelId { get; set; }

        [Required]
        public string ModelName { get; set; }

        [Required]
        public string BrandName{ get; set; }
        public string? SerialNo { get; set; }
        public string? Color { get; set; }
        public string? StrapMaterial { get; set; }
        public int? WaterResistanceM { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public Status Status { get; set; }
        public string? ImageryUrl { get; set; }
        public DateTime RecievedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsSold => SaleItem != null;

        public bool HasWarranty => Warranty != null;

        public SaleItemOutDto? SaleItem { get; set; }

        public WarrantyOutDto? Warranty { get; set; }
    }
}
