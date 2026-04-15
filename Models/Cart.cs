using System.ComponentModel.DataAnnotations;

namespace EkartAPI.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        public DateTime CreatedDate { get; set; }
    }

    public class AddToCartDTO
    {

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }
    }

    public class CartItemDTO
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
    }

    public class WishList
    {
        [Key]
        public int WishId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
    public class PostToWishList
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class WishItemDTO
    {
        public int WishId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
       
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
    }

    public class AddToCartDTOFK
    {

        [Required]
        public int product_id { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}

