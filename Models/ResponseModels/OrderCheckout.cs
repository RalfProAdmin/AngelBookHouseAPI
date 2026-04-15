namespace EkartAPI.Models.ResponseModels
{
    public class OrderCheckout
    {
        public int userId { get; set; }
        public List<OrderProductDto> ProductDetails { get; set; }
        public int AddressId { get; set; }
        public string? DeliveryDescription { get; set; }
        //public string? DeliveryInterval { get; set; }
        public string PaymentMethod { get; set; }
        public int Totalprices { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedAt { get; set; }
     
    }

    public class OrdersCheckout
    {
        public int OrderId { get; set; } = 0;
        public int userId { get; set; }
        public List<OrderProductDto> ProductDetails { get; set; } = new();
        public int AddressId { get; set; }
        public string? DeliveryDescription { get; set; }
        public string PaymentMethod { get; set; } = "";
        public int Totalprices { get; set; }
        public string Status { get; set; } = "PENDING";
        public string? RazorpayOrderId { get; set; }   // make nullable
        public string? FailureReason { get; set; }     // make nullable
    }


    public class OrderProductDto
    {
        public int ProductId { get; set; }
        public int quantity { get; set; }
 
    }

    public class OrderAddress
    {
        public int Id { get; set; }
        public int OrderId { get; set; }

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


        public bool IsActive { get; set; }

        public DateTime createAt { get; set; }

        public DateTime updateAt { get; set; }

    }
}
