namespace EkartAPI.Models
{
    public class SectionFKResponseModel
    {
        public SectionFK data { get; set; }
        public int total { get; set; }
    }
    public class SectionFK
    {
        public int[] topSellingIds { get; set; }
        public int[] trendingProductIds { get; set; }
        public int[] recentlyAddedIds { get; set; }
    }
}
