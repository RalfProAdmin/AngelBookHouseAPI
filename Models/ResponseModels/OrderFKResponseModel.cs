namespace EkartAPI.Models.ResponseModels
{

    public class OrderFKResponseModel
    {
        public List<OrderFK> data { get; set; }
        public int total { get; set; }


    }


    public class OrderFK
    {
        public int order_id { get; set; }
        public int order_number { get; set; }
        public int consumer_id { get; set; }
        public int shipping_address_id { get; set; }
        public int billing_address_id { get; set; }
        public string delivery_description { get; set; }
        public string payment_method { get; set; }
        public string payment_status { get; set; }
        public string created_at { get; set; }
        public decimal total { get; set; }
        public List<OrderProductFKDto> products { get; set; }  // Changed to a list
        public OrderAddressFKDto billing_address { get; set; }
        public OrderAddressFKDto shipping_address { get; set; }
        public subordersFkResponseModel sub_orders { get; set; }
        public orderedStatus order_status { get; set; }
    }

    public class OrderCancelledFKResponseModel
    {
        public List<OrdercancelledFK> data { get; set; }
        public int total { get; set; }


    }
    public class OrdercancelledFK
    {
        public int order_id { get; set; }
        public int order_number { get; set; }
        public int consumer_id { get; set; }
        public int shipping_address_id { get; set; }
        public int billing_address_id { get; set; }
        public string delivery_description { get; set; }
        public string payment_method { get; set; }
        public string payment_status { get; set; }
        public string created_at { get; set; }
        public decimal total { get; set; }
        public List<OrderProductFKDto> products { get; set; }  // Changed to a list
        public OrderAddressFKDto billing_address { get; set; }
        public OrderAddressFKDto shipping_address { get; set; }
        public subordersFkResponseModel sub_orders { get; set; }
        public orderedStatus order_status { get; set; }
        public OrderRefundDto refund { get; set; }
    }



    public class OrderProductFKDto
    {

        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        public AttachmentFK product_thumbnail { get; set; }

        public decimal sale_price { get; set; }
        public int discount { get; set; }
        public int quantity { get; set; }
        public string stock_status { get; set; }
        public string unit { get; set; }
        public string slug { get; set; }
        public string type { get; set; }
        public int weight { get; set; }
        public bool status { get; set; }
    }

    public class AttachmentFK
    {
        public string original_url { get; set; }
    }

    public class OrderAddressFKDto
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

    public class subordersFkResponseModel
    {

        public List<subordersFk> subordersFk { get; set; } = new List<subordersFk>();
        public int length { get; set; }


    }

    public class subordersFk
    {

    }
    public class orderedStatus
    {
        public int id { get; set; }
        public int orderId { get; set; }
        public string sequence { get; set; }
        public string name { get; set; }
        public string slug { get; set; }
        public bool status { get; set; }

    }
}

