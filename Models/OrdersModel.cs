using System.ComponentModel.DataAnnotations;

namespace EkartAPI.Models
{
    public class OrdersModel
    {
        [Key]
        public int orderId { get; set; }
        public int userId { get; set; }
        public string productId { get; set; }
        public int addressId { get; set; }
        public string paymentMethod { get; set; }
        public string quantity { get; set; }
        public int price { get; set; }
        public string PaymentStatus {  get; set; }
        public string OrderStatus { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
