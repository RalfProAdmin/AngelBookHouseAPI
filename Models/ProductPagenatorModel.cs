namespace EkartAPI.Models
{
    public class ProductPagenatorModel
    {
        public int RowNumber { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string Offer { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string Availability { get; set; }
        public string DeliverySpan { get; set; }
        public string Benefits { get; set; }
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
