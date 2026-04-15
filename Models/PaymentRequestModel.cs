namespace EkartAPI.Models
{
    public class PaymentRequestModel
    {
        public decimal Amount { get; set; }
        public int OrderId { get; set; }  // unique order reference in your DB
    }

}
