namespace EkartAPI.Models.ResponseModels
{
    public class CartItemDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class CartItem
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedDate { get; set; }
    }

}
