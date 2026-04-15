namespace EkartAPI.Models.ResponseModels
{
    public class OrderDetailsPost
    {
        public int OrderId { get; set; }
        public string FullName { get; set; }
        public DateTime DispatchDate { get; set; }
        public string PostOfficeBranch { get; set; }
        public string? TrackingNumber { get; set; }
        public DateTime ExpectedDeliveryDate { get; set; }
        public string SenderContact { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
