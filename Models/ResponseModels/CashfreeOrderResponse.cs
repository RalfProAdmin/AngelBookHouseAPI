using Newtonsoft.Json;

namespace EkartAPI.Models.ResponseModels
{
    public class CashfreeOrderResponse
    {
        [JsonProperty("cf_order_id")]
        public string CfOrderId { get; set; }

        [JsonProperty("order_id")]
        public string OrderId { get; set; }

        [JsonProperty("entity")]
        public string Entity { get; set; }

        [JsonProperty("order_currency")]
        public string OrderCurrency { get; set; }

        [JsonProperty("order_amount")]
        public decimal OrderAmount { get; set; }

        [JsonProperty("order_status")]
        public string OrderStatus { get; set; }

        [JsonProperty("payment_session_id")]
        public string PaymentSessionId { get; set; }

        [JsonProperty("order_expiry_time")]
        public string OrderExpiryTime { get; set; }

        [JsonProperty("order_note")]
        public string OrderNote { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("customer_details")]
        public CustomerDetailsResponse CustomerDetails { get; set; }
    }

    public class CustomerDetailsResponse
    {
        [JsonProperty("customer_id")]
        public string CustomerId { get; set; }

        [JsonProperty("customer_email")]
        public string CustomerEmail { get; set; }

        [JsonProperty("customer_phone")]
        public string CustomerPhone { get; set; }
    }
}
