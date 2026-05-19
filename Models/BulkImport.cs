using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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

        [JsonIgnore]
        public List<Watch> Watches { get; set; } = [];
    }
}
