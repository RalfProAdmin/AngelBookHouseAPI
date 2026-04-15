namespace EkartAPI.Models
{
    public class CategoryFKResponseModel
    {
        public List<CategoryFK> data { get; set; }
        public int total { get; set; }
    }
    public class CategoryFK
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string slug { get; set; }
        public string type { get; set; }
        public int parent_id { get; set; }
        public int products_count { get; set; }
        public bool status { get; set; }
    }
}
