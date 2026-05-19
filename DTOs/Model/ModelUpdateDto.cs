using DialDesk.Server.Models;

namespace DialDesk.Server.DTOs.Model;

public class ModelUpdateDto
{
    public string? ModelNo { get; set; }

    public Category? Category { get; set; }

    public int? BrandId { get; set; }

    public string? ModelName { get; set; }

    public int? LowStockThreshold { get; set; }

    public string? Description { get; set; }

    public string? ImageryUrl { get; set; }

    public bool? IsActive { get; set; }
}
