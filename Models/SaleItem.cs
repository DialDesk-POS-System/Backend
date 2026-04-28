using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DialDesk.Server.Models
{
    public class SaleItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int SaleId { get; set; }

        [ForeignKey("SaleId")]
        public Sale Sale { get; set; }

        public Warranty? Warranty { get; set; }

        [Required]
        public string WatchId { get; set; }
        public Watch Watch { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        [Required]
        public decimal CostPrice { get; set; }

        [Required]
        public decimal DiscountAmount { get; set; } = 0;

        [Required]
        public decimal LineTotal { get; set; }

        [InverseProperty("OriginalSaleItem")]
        public Return? ReturnAsOriginal { get; set; }

        [InverseProperty("NewSaleItem")]
        public Return? ReturnAsNew { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }


    }
}
