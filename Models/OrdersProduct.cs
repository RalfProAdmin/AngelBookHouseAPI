using EkartAPI.Models.ResponseModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace EkartAPI.Models
{
    public class OrdersProduct
    {
        [Key]
        public int OrderedProductId { get; set; }   // Primary Key (Identity in DB)

        public int OrderId { get; set; }            // FK to OrderDetails
        public int ProductId { get; set; }          // FK to tbl_products
        public string ProductName { get; set; }     // Name of the product
        public string Description { get; set; }     // Description
        public int Quantity { get; set; }           // Quantity ordered
        public decimal Price { get; set; }          // Price at the time of order
        public string ImageUrl { get; set; }        // Product image
    }
}
