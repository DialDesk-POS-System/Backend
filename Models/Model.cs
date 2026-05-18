using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DialDesk.Server.Models
{
    [Index(nameof(ModelNo), IsUnique = true)]
    [Index(nameof(ModelName), IsUnique = true)]
    public class Model
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ModelNo { get; set; }

        [Required]
        public Category Category { get; set; }

        public int BrandId { get; set; }
        [ForeignKey("BrandId")]
        public required Brand Brand { get; set; }

        [Required]
        public string ModelName { get; set; }
        public int? LowStockThreshold { get; set; }

        public string? Description { get; set; }

        public string ImageryUrl {  get; set; }

        public Boolean IsActive { get; set; } = true;

        public List<Watch> Watches { get; set; } = [];
        public List<ModelPriceHistory> PriceHistoryRecords { get; set; } = [];
    }
}
