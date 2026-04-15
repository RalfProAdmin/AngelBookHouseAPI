namespace EkartAPI.Models.ResponseModels
{
    public class DeliveryAddressFKResponseGet
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public string Title { get; set; }
        public string Street { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public int Pincode { get; set; }
        public string Phone { get; set; }

    }
}
