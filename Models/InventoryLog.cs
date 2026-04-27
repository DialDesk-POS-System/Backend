using System.ComponentModel.DataAnnotations;

namespace DialDesk.Server.Models
{
    public class InventoryLog
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string WatchId { get; set; }

        [Required]
        public ChangeType ChangeType { get; set; }

        public string? Notes { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }



    }
}
