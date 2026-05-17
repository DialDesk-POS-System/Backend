using System.ComponentModel.DataAnnotations;
using DialDesk.Server.Models;

namespace DialDesk.Server.DTOs.Model
{
    public class ModelInDto
    {
        [Required]
        public string ModelNo { get; set; }

        [Required]
        public Category Category { get; set; }

        [Required]
        public int BrandId { get; set; }

        [Required]
        public string ModelName { get; set; }

        [Required]
        public decimal BasePrice { get; set; }

        public string? Description { get; set; }

        public string ImageryUrl { get; set; }

        public bool IsActive { get; set; }
    }
}
