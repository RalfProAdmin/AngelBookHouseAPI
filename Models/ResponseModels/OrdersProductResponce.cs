namespace EkartAPI.Models.ResponseModels
{
    public class OrdersProductResponce
    {

        public int OrderId { get; set; }           // FK to OrderDetails
        public int ProductId { get; set; }         // FK to tbl_products
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
    }
}
