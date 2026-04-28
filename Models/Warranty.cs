using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DialDesk.Server.Models
{
   
    public class Warranty
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int SaleItemId { get; set; }

        [ForeignKey("SaleItemId")]
        public SaleItem SaleItem { get; set; }

        [Required]
        public string WatchId { get; set; }

        [ForeignKey("WatchId")]
        public Watch Watch { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public DateTime? ClaimDate { get; set; }


    }
}
