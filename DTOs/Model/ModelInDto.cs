using DialDesk.Server.Models;
using System.ComponentModel.DataAnnotations;

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

        public bool IsActive { get; set; }
    }
}
