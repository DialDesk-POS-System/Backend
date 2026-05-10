using System.ComponentModel.DataAnnotations;

namespace DialDesk.Server.DTOs.Brand
{
    public class BrandOutDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string ManufacturedCountry { get; set; }

        public string LogoUrl { get; set; }

        public bool IsActive { get; set; }
    }
}
