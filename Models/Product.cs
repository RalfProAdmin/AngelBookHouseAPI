namespace EkartAPI.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string Offer { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public string Availability { get; set; }
        public bool IsUsed { get; set; }
        public bool Enquiry { get; set; }
        public string DeliverySpan { get; set; }
        public string Benefits { get; set; }
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public bool topSelling { get; set; }
        public bool trendingProduct { get; set; }
        public bool recentlyAdded { get; set; }

        public int GetDiscount()
        {
            if (string.IsNullOrEmpty(Offer))
                return 0;

            return int.TryParse(Offer.Replace("%", "").Replace("OFF", "").Trim(), out int discount) ? discount : 0;
        }
    }


}
