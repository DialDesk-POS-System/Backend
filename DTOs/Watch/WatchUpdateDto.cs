using DialDesk.Server.Models;
using System.ComponentModel.DataAnnotations;

namespace DialDesk.Server.DTOs.Watch
{
    public class WatchUpdateDto
    {
        [Required]
        public int ModelId { get; set; }

        [Required]
        public int ImportId { get; set; }

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
