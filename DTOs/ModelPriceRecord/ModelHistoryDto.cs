using System.ComponentModel.DataAnnotations;

namespace DialDesk.Server.DTOs
{
    public class ModelHistoryDto
    {
        [Required]
        public decimal PurchasePrice { get; set; }

        [Required]
        public int ModelId { get; set; }

        [Required] public string ModelName { get; set; }

        [Required]
        public decimal SellingPrice { get; set; }

        [Required]
        public DateTime EffectiveFrom { get; set; }

        [Required]
        public DateTime EffectiveTo { get; set; }

        public string? Notes { get; set; }
    }
}
