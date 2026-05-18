namespace DialDesk.Server.DTOs.ModelPriceRecord;

public class ModelHistoryUpdateDto
{
    public decimal? PurchasePrice { get; set; }

    public int? ModelId { get; set; }

    public string? ModelName { get; set; }

    public decimal? SellingPrice { get; set; }

    public DateTime? EffectiveFrom { get; set; }

    public DateTime? EffectiveTo { get; set; }

    public string? Notes { get; set; }
}
