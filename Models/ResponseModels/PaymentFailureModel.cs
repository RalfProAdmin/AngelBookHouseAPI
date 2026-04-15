namespace EkartAPI.Models.ResponseModels
{
    public class PaymentFailureModel
    {
        public string razorpay_order_id { get; set; }
        public string reason { get; set; }
    }
}
