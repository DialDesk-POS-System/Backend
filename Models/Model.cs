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
        public Category Category { get; set; }

        public int BrandId { get; set; }
        [ForeignKey("BrandId")]
        public required Brand Brand { get; set; }

        [Required]
        public string ModelName { get; set; }

        [Required]
        public int WarrantyPeriod { get; set; }

        public Boolean IsActive { get; set; }

        public List<Watch> Watches { get; set; } = [];
    }
}
