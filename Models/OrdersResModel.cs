namespace EkartAPI.Models
{
    public class OrdersResModel
    {
        public int orderId { get; set; }
        public int userId { get; set; }
        public string productId { get; set; }
        public int addressId { get; set; }
        public string productName { get; set; }
        public string imageurl { get; set; }
        public string DeliverySpan { get; set; }
        public string paymentMethod { get; set; }

        public string PaymentStatus { get; set; }
        public string OrderStatus { get; set; }
        public string quantity { get; set; }
        public decimal price { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
