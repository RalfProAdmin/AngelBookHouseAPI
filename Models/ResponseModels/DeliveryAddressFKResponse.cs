using System.Diagnostics.Metrics;

namespace EkartAPI.Models.ResponseModels
{
    public class DeliveryAddressFKResponse
    {
        public string Title { get; set; }
        public string Street { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public int Pincode { get; set; }
        public string Phone { get; set; }
    }
}
