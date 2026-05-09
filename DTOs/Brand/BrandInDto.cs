using System.ComponentModel.DataAnnotations;

namespace DialDesk.Server.DTOs.Brand
{
    public class BrandInDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string ManufacturedCountry { get; set; }

        public string? LogoUrl { get; set; }

        public bool IsActive { get; set; }
    }
}
