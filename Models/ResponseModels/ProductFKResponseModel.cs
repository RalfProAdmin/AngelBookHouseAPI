namespace EkartAPI.Models
{
    public class ProductFKResponseModel
    {
        public List<ProductFK> data { get; set; }
        public int total { get; set; }
    }
    public class ProductFK
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int price { get; set; }
        public Attachment product_thumbnail { get; set; }
        public int sale_price { get; set; }
        public int discount { get; set; }
        public string stock {  get; set; }
        public string stock_status { get; set; }
        public bool Enquiry { get; set; }
        public bool IsUsed { get; set; }
        public string unit { get; set; }
        public string slug { get; set; }
        public string type { get; set; }
        public int weight { get; set; }
        public bool status { get; set; }
        public List<Category> categories { get; set; }
    }

    public class Attachment
    {
        public string original_url { get; set; }
    }

    public class Category
    {
        public string slug { get; set; }
    }
}
