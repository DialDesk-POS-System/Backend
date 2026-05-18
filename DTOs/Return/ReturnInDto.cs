namespace DialDesk.Server.DTOs.Return
{
    public class ReturnInDto
    {
        public int OriginalSaleItemId { get; set; }
        public int? NewSaleItemId { get; set; }
        public decimal RefundAmount { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
