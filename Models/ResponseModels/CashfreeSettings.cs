namespace EkartAPI.Models.ResponseModels
{
    public class CashfreeSettings
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Environment { get; set; } // sandbox or production
    }

}
