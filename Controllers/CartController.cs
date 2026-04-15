using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EkartAPI.Models;
using EkartAPI.Models.ResponseModels;
using EkartAPI.Repository.Implementation;
using EkartAPI.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Razorpay.Api;

namespace EkartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;

        public CartController(ICartRepository cartRepository, IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
        }



        [HttpPost("update-quantity")]
        public async Task<IActionResult> UpdateCartQuantity([FromBody] AddToCartDTOFK request)
        {
            var userIdClaim = User.FindFirst("userId");

            if (!int.TryParse(userIdClaim.Value, out int userId))
            {
                return BadRequest(new { message = "Invalid User ID format" });
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _cartRepository.UpdateCartQuantity(userId, request.product_id, request.Quantity);
            return Ok(new { Message = "Cart updated successfully" });
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartDTOFK request)
        {

            var userIdClaim = User.FindFirst("userId");

            if (!int.TryParse(userIdClaim.Value, out int userId))
            {
                return BadRequest(new { message = "Invalid User ID format" });
            }
            // Fetch product details to get availability
            var product = await _productRepository.GetProductById(request.product_id);
            if (product == null)
            {
                return NotFound("Product not found.");
            }

            int availableStock;
            if (!int.TryParse(product.Availability, out availableStock))
            {
                return BadRequest("Invalid product availability value.");
            }


            // Retrieve the cart details for the specific user and product
            var cartDetails = await _cartRepository.GetCartDetailsByUserAndProductId(userId, request.product_id);
            if (cartDetails != null)
            {
                // Calculate the total quantity including the new request
                int quantityTotal = cartDetails.Quantity + request.Quantity;

                if (quantityTotal == 0)
                {
                    // Delete when user decreases quantity from 1 to 0
                    await _cartRepository.DeleteCartItemAsync(cartDetails.CartId);
                }
                else if (quantityTotal > availableStock)
                {
                    return BadRequest($"Requested quantity exceeds available stock. Only {availableStock} items left.");
                }
                else
                {
                    // Update quantity
                    cartDetails.Quantity = quantityTotal;
                    cartDetails.CreatedDate = DateTime.UtcNow;
                    await _cartRepository.UpdateCartItemAsync(cartDetails);
                }

                // Prepare the response DTO
                var response = new CartItemDTO
                {
                    CartId = cartDetails.CartId,
                    UserId = cartDetails.UserId,
                    ProductId = cartDetails.ProductId,
                    Quantity = cartDetails.Quantity,
                    CreatedDate = cartDetails.CreatedDate
                };

                return Ok(response);
            }
            else
            {
                if (request.Quantity > availableStock)
                {
                    return BadRequest($"Requested quantity exceeds available stock. Only {availableStock} items left.");
                }

                // Create a new cart item if it doesn't already exist
                var cart = new Cart
                {
                    UserId = userId,
                    ProductId = request.product_id,
                    Quantity = request.Quantity,
                    CreatedDate = DateTime.UtcNow
                };

                await _cartRepository.AddToCartAsync(cart);

                // Prepare the response DTO
                var response = new CartItemDTO
                {
                    CartId = cart.CartId,
                    UserId = cart.UserId,
                    ProductId = cart.ProductId,
                    Quantity = cart.Quantity,
                    CreatedDate = cart.CreatedDate
                };

                return Ok(response);
            }
        }



        [HttpGet("{userId:int}")]
        [Authorize]
        public async Task<IActionResult> GetAllCartItems(int userId)
        {
            var cartItems = await _cartRepository.GetAllCartItemsAsync(userId);
            return Ok(cartItems);
        }
        [HttpPut("{userId:int}/{productId:int}")]
        [Authorize]
        public async Task<IActionResult> UpdateCartItem( int productId, [FromBody] AddToCartDTO request)
        {
            var userIdClaim = User.FindFirst("userId");

            if (!int.TryParse(userIdClaim.Value, out int userId))
            {
                return BadRequest(new { message = "Invalid User ID format" });
            }
            var existingCart = await _cartRepository.GetCartDetailsByUserAndProductId(userId, productId);
            if (existingCart == null)
            {
                return NotFound();
            }

            existingCart.Quantity = request.Quantity;
            existingCart.CreatedDate = DateTime.UtcNow;

            if (request.Quantity == 0)
            {
                // If the quantity is set to zero, delete the cart item
                await _cartRepository.DeleteCartItemAsync(existingCart.CartId);
                var response = new CartItemDTO
                {
                    Quantity = 0
                };
                return Ok(response);
            }
            else
            {
                // Update the cart item with the new quantity
                await _cartRepository.UpdateCartItemAsync(existingCart);

                var response = new CartItemDTO
                {
                    CartId = existingCart.CartId,
                    UserId = existingCart.UserId,
                    ProductId = existingCart.ProductId,
                    Quantity = existingCart.Quantity,
                    CreatedDate = existingCart.CreatedDate,
                    // Include product details if necessary
                };

                return Ok(response);
            }
        }

        [HttpGet("detailsFK")]
        [Authorize]
        public async Task<IActionResult> GetCartDetails()
        {
            var userIdClaim = User.FindFirst("userId");

            if (!int.TryParse(userIdClaim.Value, out int userId))
            {
                return BadRequest(new { message = "Invalid User ID format" });
            }
            var cartDetails = await _cartRepository.GetCartItemByIdFKAsync(userId);

            if (cartDetails == null )
            {
                return NotFound(new { message = "Cart is empty for this user." });
            }

            return Ok(cartDetails);
        }

        [HttpPost("sync")]
        [Authorize]
        public async Task<IActionResult> SyncCart([FromBody] List<CartItemDto> cartItemDtos)
        {
            if (cartItemDtos == null || !cartItemDtos.Any())
                return BadRequest("Cart is empty");

            var userIdClaim = User.FindFirst("userId");

            if (!int.TryParse(userIdClaim.Value, out int userId))
            {
                return BadRequest(new { message = "Invalid User ID format" });
            }
            var cartItems = cartItemDtos.Select(dto => new CartItem
            {
                UserId = userId,
                ProductId = dto.ProductId,
                Quantity = dto.Quantity,
                CreatedDate = DateTime.UtcNow
            }).ToList();

            await _cartRepository.InsertCartItemsAsync(cartItems);

            return Ok(new { message = "Cart items inserted successfully" });
        }





        [HttpDelete("{cartId:int}")]
        [Authorize]
  
        public async Task<IActionResult> DeleteCartItem(int cartId)
        {
            var existingCart = await _cartRepository.GetAllCartItemsAsync(cartId);
            if (existingCart == null)
            {
                return NotFound();
            }

            await _cartRepository.DeleteCartItemAsync(cartId);

            return NoContent();
        }
    }
}
