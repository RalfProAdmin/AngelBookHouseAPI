using EkartAPI.Models;
using EkartAPI.Repository.Implementation;
using EkartAPI.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EkartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishListController : ControllerBase
    {

        private readonly IOrdersRepository _ordersRepository;
        private readonly IProductRepository _productRepository;
        private readonly IDeliveryAddressRepository _deliveryRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IConfiguration _configuration;
        private readonly IWishListRepository _wishListRepository;

        public WishListController(IOrdersRepository ordersRepository, IProductRepository productRepository, ICartRepository cartRepository,
            IDeliveryAddressRepository deliveryRepository, IConfiguration configuration, IWishListRepository wishListRepository)
        {
            _ordersRepository = ordersRepository;
            _productRepository = productRepository;
            _cartRepository = cartRepository;
            _deliveryRepository = deliveryRepository;
            _configuration = configuration;
            _wishListRepository = wishListRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddItemsToWishList([FromBody] PostToWishList wishItem)
        {
            // Call the repository method to add item to wishlist
            var message = await _wishListRepository.AddToWishListAsync(wishItem.UserId, wishItem.ProductId);

            if (message == "Product added to wishlist successfully.")
            {
                return Ok(new { Message = message });
            }
            else
            {
                return BadRequest(new { Message = message });
            }
        }


        [HttpGet("{userId:int}")]
        [Authorize]
        public async Task<IActionResult> GetWishlistItems(int userId)
        {
            var wishlistItems = await _wishListRepository.GetAllWishlistItemsAsync(userId);
            return Ok(wishlistItems);
        }

        [HttpGet("GetAllWishlistItems")]
        [Authorize]
        public async Task<IActionResult> GetAllWishlistItemsFK()
        {
          
            var userIdClaim = User.FindFirst("userId");
           
            if (!int.TryParse(userIdClaim.Value, out int userId))
            {
                return BadRequest(new { message = "Invalid User ID format" });
            }

            var result = await _wishListRepository.GetAllWishlistItemsFKAsync(userId);
            return Ok(result);
        }



        [HttpDelete("{productId:int}")]
        public async Task<IActionResult> DeleteWishItem([FromRoute] int productId)
        {
            // Assuming DeleteByProductId is a method that deletes the wishlist item by productId
            var isDeleted = await _wishListRepository.DeleteWishItem(productId);

            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }


        [HttpGet("WishListstatus")]
        public async Task<IActionResult> UpdateOrderStatus(int userid, int productId)
        {

            var resultMessage = await _wishListRepository.GetProductExistOrNotInWishList(userid, productId);

            if (resultMessage.Contains("Exist") || resultMessage.Contains("Doesn't Exist"))
            {
                return Ok(new { Message = resultMessage });
            }

            return NotFound(new { Message = resultMessage });
        }

        [HttpGet]
        [Route("wishlist/{userId}/{productId}")]
        public async Task<IActionResult> SetWishListItems([FromRoute] int userId, [FromRoute] int productId)
        {
            var existingWishlist = await _wishListRepository.GetWishlistDetailsByUserAndProductId(userId, productId);
            if (existingWishlist == null)
            {
                return NotFound();
            }

            var response = new
            {
                WishId = existingWishlist.WishId
            };

            return Ok(response);
        }


    }
}