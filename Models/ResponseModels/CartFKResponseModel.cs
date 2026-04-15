using System.ComponentModel.DataAnnotations.Schema;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace EkartAPI.Models
{
    public class CartFKResponseModel
    {
        public List<CartFK> items { get; set; }
        public decimal total { get; set; }
        public bool stickyCartOpen { get; set; }
        public bool sidebarCartOpen { get; set; }
    }

    public class CartFK
    {
        public int id { get; set; }
        public int product_id { get; set; }
        public int? consumer_id { get; set; }
        public int quantity { get; set; }
        public decimal sub_total { get; set; }
        public decimal price { get; set; }
        [NotMapped]
        public Variation variation { get; set; }
        [NotMapped]
        public ProductsFK product { get; set; }
    }

    public class Variation
    {
        public decimal price { get; set; }
        public decimal sale_price { get; set; }
        public string name { get; set; }
    }

    public class ProductsFK
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        [NotMapped]
        public Attachment product_thumbnail { get; set; }
        public decimal sale_price { get; set; }
        
    }

}
