namespace DialDesk.Server.DTOs.Brand;

public class BrandUpdateDto
{
    public string? Name { get; set; }

    public string? ManufacturedCountry { get; set; }

    public string? LogoUrl { get; set; }

    public bool? IsActive { get; set; }
}
