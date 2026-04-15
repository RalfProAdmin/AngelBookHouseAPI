using System.ComponentModel.DataAnnotations;

namespace EkartAPI.Models.ResponseModels
{
    public class ProductResponceModel
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string Offer { get; set; }          // Assuming Offer is a percentage/discount value
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public string Availability { get; set; }       // Assuming this represents quantity in stock
        public string DeliverySpan { get; set; }
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }    // Nullable if it might be null
    }
}
