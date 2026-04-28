using System.ComponentModel.DataAnnotations;

namespace DialDesk.Server.Models
{
    public class ModelPriceHistory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public decimal PurchasePrice { get; set; }

        public int ModelId { get; set; }
        [ForeignKey("ModelId")]
        public Model Model { get; set; }

        [Required]
        public decimal SellingPrice { get; set; }

        [Required]
        public DateTime EffectiveFrom { get; set; }

        [Required]
        public DateTime EffectiveTo { get; set; }

        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
