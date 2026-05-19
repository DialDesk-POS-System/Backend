using DialDesk.Server.Models;

namespace DialDesk.Server.DTOs.Watch;

public class WatchSearchDto
{
    public string? SearchQuery { get; set; }
    public string? ModelName { get; set; }
    public string? ModelNo { get; set; }
    public string? BrandName { get; set; }
    public Category? Category { get; set; }
    public string? SerialNo { get; set; }
    public string? Color { get; set; }
    public string? StrapMaterial { get; set; }
}
