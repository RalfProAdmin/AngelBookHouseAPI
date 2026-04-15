namespace EkartAPI.Models.ResponseModels
{
    public class ProductStatusUpdateModel
    {
        public int ProductId { get; set; }
        public bool TopSelling { get; set; }
        public bool TrendingProduct { get; set; }
        public bool RecentlyAdded { get; set; }
    }
}
