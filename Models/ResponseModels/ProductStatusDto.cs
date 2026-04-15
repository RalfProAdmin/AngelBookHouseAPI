namespace EkartAPI.Models.ResponseModels
{
    public class ProductStatusDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public bool TopSelling { get; set; }
        public bool trendingProduct { get; set; }
        public bool RecentlyAdded { get; set; }
    }
}
