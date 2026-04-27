using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DialDesk.Server.Models
{
    public class Model
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ModelNo { get; set; }

        [Required]
        public Category category { get; set; }

        public int BrandId { get; set; }
        [ForeignKey("BrandId")]
        public required Brand Brand { get; set; }

        [Required]
        public string ModelName { get; set; }

        [Required]
        public decimal BasePrice { get; set; }

        [Required]
        public int WarrantyPeriod { get; set; }

        public Boolean IsActive { get; set; }

        [InverseProperty("Model")]
        public List<Watch> Watches { get; set; } = [];
    }
}
