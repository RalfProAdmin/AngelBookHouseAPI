namespace EkartAPI.Models.ResponseModels
{
    public class OrderDetails
    {
        public int OrderId { get; set; }
        public int UserID { get; set; }
        public string DeliveryDescription { get; set; }
        public string PaymentMethod { get; set; }
        public int TotalPrice { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public string RazorpayOrderId { get; set; } 
        public string? FailureReason { get; set; }
    }
}
