using EkartAPI.Models;

namespace EkartAPI.Models
{
    public class ResentOrdersModel
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
        public string FullName { get; set; }

        public string MobileNumber { get; set; }

        public string AlternateMobileNumber { get; set; }


        public int PinCode { get; set; }

        public string HouseNo { get; set; }


        public string AreaDetails { get; set; }

        public string Landmark { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string TypeOfAddress { get; set; }
        public string quantity { get; set; }
        public decimal price { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}