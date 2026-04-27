using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DialDesk.Server.Models
{
    public class Watch
    {
        [Key]
        public string Id { get; set; }

        public int ModelId { get; set; }
        [ForeignKey("ModelId")]
        public required Model Model { get; set; }

        public string? SerialNo { get; set; }

        public string? Color { get; set; }

        public string? StrapMetrial { get; set; }

        public int? WaterResistanceM { get; set; }

        [Required]
        public decimal CostPrice { get; set; }

        [Required]
        public decimal SellingPrice { get; set; }

        [Required]
        public Status Status { get; set; }

        public string? ImageryUrl { get; set; }

        public DateTime RecievedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

    }
}
