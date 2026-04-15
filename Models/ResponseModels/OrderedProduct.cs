namespace EkartAPI.Models.ResponseModels
{
    public class OrderedProduct
    {
        public int OrderedProductId { get; set; }
        public int OrderID { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
    }

    // Models/ShippingAddress.cs
    public class ShippingAddress
    {
        public int ShippingAddressId { get; set; }
        public int OrderID { get; set; }
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
    }

    public class OrderDeliveryDetails
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string FullName { get; set; }
        public DateTime DispatchDate { get; set; }
        public string PostOfficeBranch { get; set; }
        public string TrackingNumber { get; set; }
        public DateTime ExpectedDeliveryDate { get; set; }
        public string SenderContact { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }


    // Models/OrderDetailsDTO.cs
    public class OrderDetailsDTO
    {
        public List<OrderedProduct> OrderedProducts { get; set; }
        public ShippingAddress ShippingAddress { get; set; }
        public OrderDeliveryDetails DeliveryDetails { get; set; }
    }

    public class OrderRefundDetailsDTO
    {
        public List<OrderedProduct> OrderedProducts { get; set; }
        public ShippingAddress ShippingAddress { get; set; }
        public CancelOrderRequest OrderRefundDetails { get; set; }
    }


}