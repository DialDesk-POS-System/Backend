using DialDesk.Server.DTOs.Watch;

namespace DialDesk.Server.DTOs.BulkImport
{
    public class BulkImportCreateDto
    {
        public string? Supplier { get; set; }

        public DateTime ImportDate { get; set; }

        public int TotalItems { get; set; }

        public List<WatchCreateDto> Watches{ get; set; } = new();
    }
}
