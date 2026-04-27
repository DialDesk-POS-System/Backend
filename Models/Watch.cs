using System.ComponentModel.DataAnnotations;

namespace DialDesk.Server.Models
{
    public class Watch
    {
        [Key]
        public string sku_id { get; set; }

        public string model_id {get; set; }

        public string? serial_no { get; set; }

        public string? color { get; set; }

        public string? strap_metrial { get; set; }

        public DateTime recieved_at { get; set; }

        public DateTime Updated_at { get; set; }
    }
}
