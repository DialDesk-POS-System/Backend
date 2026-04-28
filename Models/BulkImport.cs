using System.ComponentModel.DataAnnotations;

namespace DialDesk.Server.Models
{
    public class BulkImport
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime ImportDate { get; set; }

        [Required]
        public int TotalItems { get; set; }

        public string? Supplier { get; set; }

        public List<Watch> Watches { get; set; } = [];
    }
}
