using DialDesk.Server.Models;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace DialDesk.Server.DTOs.Watch
{
    public class WatchCreateDto
    {
     
        [Required]
        public int ModelId { get; set; }

        [Required]
        public int ImportId { get; set; }

        public int? Quantity { get; set; }

        public string? SerialNo { get; set; }

        public string? Color { get; set; }

        public string? StrapMaterial { get; set; }

        public int? WaterResistanceM { get; set; }

        [Required]
        public decimal CostPrice { get; set; }

        [Required]
        public decimal SellingPrice { get; set; }

        public string? ImageryUrl { get; set; }
    }
}
