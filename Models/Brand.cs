using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DialDesk.Server.Models
{
    public class Brand
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string ManufacturedCountry { get; set; }

        public string LogoUrl { get; set; }
        public Boolean IsActive { get; set; }

        public List<Model> Models { get; set; } = [];
    }
}
