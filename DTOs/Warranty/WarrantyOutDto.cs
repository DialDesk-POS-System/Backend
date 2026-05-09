namespace DialDesk.Server.DTOs
{
    public class WarrantyOutDto
    {
        public int Id { get; set; }

        public int SaleItemId { get; set; }

        public string WatchId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime? ClaimDate { get; set; }

        public bool IsClaimed => ClaimDate != null;

        public bool IsExpired => DateTime.Now > EndDate;
    }
}
