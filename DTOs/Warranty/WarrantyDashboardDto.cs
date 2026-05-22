namespace DialDesk.Server.DTOs.Warranty
{
    public class WarrantyDashboardDto
    {
        public int ActiveCount { get; set; }

        public int Expiring30DaysCount { get; set; }

        public int ExpiredCount { get; set; }

        public int ClaimedCount { get; set; }
    }
}
