namespace DialDesk.Server.DTOs
{
    public class ModelPriceHistoryRecordSearch
    {
        public string? ModelName { get; set; }

        public decimal? PurchasePriceMin { get; set; }
        public decimal? PurchasePriceMax { get; set; }
        public decimal? SellingPriceMin { get; set; }
        public decimal? SellingPriceMax { get; set; }

        public DateTime? EffectiveFromStart { get; set; }
        public DateTime? EffectiveFromEnd { get; set; }
        public DateTime? EffectiveToStart { get; set; }
        public DateTime? EffectiveToEnd { get; set; }

        public DateTime? CreatedAtFrom { get; set; }
        public DateTime? CreatedAtTo { get; set; }

        public string? Notes { get; set; }
    }
}
