namespace EkartAPI.Models.ResponseModels
{
    public class OrderStatus
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string sequence { get; set; }
        public bool status { get; set; }
        public DateTime created_At { get; set; }
        public DateTime updated_At { get; set; }
    }

    public class UpdateOrderStatus
    {
        public int OrderId { get; set; }
        public string name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public bool status { get; set; }

    }
}
