namespace EkartAPI.Models.ResponseModels
{
    public class OrderRefundDto
    {
        public int OrderId { get; set; }
        public int RefundId { get; set; }
        public string Status { get; set; }
    }
}
