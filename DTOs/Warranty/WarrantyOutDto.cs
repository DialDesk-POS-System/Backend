namespace DialDesk.Server.DTOs
{
    public class WarrantyOutDto
    {
        public int Id { get; set; }

        public string InvoiceNo { get;set; }
        public int SaleItemId { get; set; }

        //public Guid WatchId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Status { get; set; }

        public DateTime? ClaimDate { get; set; }

        


    }
}
