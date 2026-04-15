namespace EkartAPI.Models.ResponseModels
{
    public class UpdateOrdersCheckout
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int AddressId { get; set; }
        public List<OrderProductDto> ProductDetails { get; set; }
        public string DeliveryDescription { get; set; }
        public string PaymentMethod { get; set; }
        public decimal Totalprices { get; set; }
        public string Status { get; set; }
        public string RazorpayOrderId { get; set; }
        public string FailureReason { get; set; }
    }
}
