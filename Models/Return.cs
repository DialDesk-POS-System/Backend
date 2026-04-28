using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DialDesk.Server.Models
{
    public class Return
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int OriginalSaleItemId { get; set; }

        [ForeignKey("OriginalSaleItemId")]
        public SaleItem OriginalSaleItem { get; set; }

        [Required]
        public int NewSaleItemId { get; set; }
        [ForeignKey("NewSaleItemId")]
        public SaleItem? NewSaleItem { get; set; }

        [Required]
        public decimal RefundAmount { get; set; }

        [Required]
        public DateTime ReturnDate { get; set; }

    }
}