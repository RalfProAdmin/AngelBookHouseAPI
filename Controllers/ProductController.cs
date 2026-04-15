using EkartAPI.Models;
using EkartAPI.Models.ResponseModels;
using EkartAPI.Repository.Implementation;
using EkartAPI.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EkartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IOrdersRepository _ordersRepository;
        private readonly IUserRepository _usersRepository;

        public ProductController(IProductRepository productRepository, IOrdersRepository ordersRepository, IUserRepository usersRepository)
        {

            _usersRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
            _ordersRepository = ordersRepository ?? throw new ArgumentNullException(nameof(ordersRepository));
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));

        }

        [HttpGet("GetAllProducts_FK")]
        public async Task<IActionResult> GetAllProducts_FK()
        {
            var products = await _productRepository.GetAllProductsAsync_FK();
            return Ok(products);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            // Fetch products using the stored procedure
            var products = await _productRepository.GetProductsByCategory();

            // Map to the desired response structure
            var response = products.Select(p => new ProductByCategoryModel
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                Description = p.Description,
                Offer = p.Offer,
                Price = p.Price,
                CategoryId = p.CategoryId,
                Level1CategoryName = p.Level1CategoryName, // Top-level category name from SP
                Availability = p.Availability,
                Enquiry = p.Enquiry,
                IsUsed = p.IsUsed,
                DeliverySpan = p.DeliverySpan,
                Benefits = p.Benefits,
                ImageUrl = p.ImageUrl,
                IsActive = p.IsActive,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt
            })
            .OrderByDescending(p => p.ProductId) // Sort the response by ProductId in descending order
            .ToList();

            return Ok(response);
        }


        [HttpGet("{id:int}")]
        
        public async Task<IActionResult> GetProductById([FromRoute] int id)
        {
            var product = await _productRepository.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            var response = new Product
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Description = product.Description,
                Offer = product.Offer,    
                Price = product.Price,
                CategoryId = product.CategoryId,
                Availability = product.Availability,
                Enquiry = product.Enquiry,
                IsUsed = product.IsUsed,
                DeliverySpan = product.DeliverySpan,
                Benefits = product.Benefits,
                ImageUrl = product.ImageUrl,
                IsActive = product.IsActive,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            return Ok(response);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {
            var addedProduct = await _productRepository.AddProduct(product);

            var response = new Product
            {
                ProductName = addedProduct.ProductName,
                Description = addedProduct.Description,
                Offer = addedProduct.Offer,
                Price = addedProduct.Price,
                CategoryId = addedProduct.CategoryId,
                Availability = addedProduct.Availability,
                DeliverySpan = addedProduct.DeliverySpan,
                Benefits = addedProduct.Benefits,
                Enquiry = addedProduct.Enquiry,
                IsUsed = addedProduct.IsUsed,
                topSelling =false,
                trendingProduct=false,
                recentlyAdded=false,


                ImageUrl = addedProduct.ImageUrl,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            return Ok(response);
        }

        [HttpPut("{id:int}")]
       
        public async Task<IActionResult> UpdateProduct([FromRoute] int id, [FromBody] Product product)
        {
            var existingProduct = await _productRepository.GetProductById(id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            existingProduct.ProductName = product.ProductName;
            existingProduct.Description = product.Description;
            existingProduct.Offer = product.Offer;
            existingProduct.Price = product.Price;
            existingProduct.Enquiry = product.Enquiry;
            existingProduct.IsUsed = product.IsUsed;
            existingProduct.CategoryId = product.CategoryId;
            existingProduct.Availability = product.Availability;
            existingProduct.DeliverySpan = product.DeliverySpan;
            existingProduct.Benefits = product.Benefits;
            existingProduct.ImageUrl = product.ImageUrl;
            existingProduct.IsActive = true;
            existingProduct.UpdatedAt = DateTime.UtcNow;

            var updatedProduct = await _productRepository.UpdateProduct(existingProduct);

            var response = new Product
            {
                ProductId = updatedProduct.ProductId,
                ProductName = updatedProduct.ProductName,
                Description = updatedProduct.Description,
                Offer = updatedProduct.Offer,
                Price = updatedProduct.Price,
                Enquiry = updatedProduct.Enquiry,
                IsUsed = updatedProduct.IsUsed,
                CategoryId = updatedProduct.CategoryId,
                Availability = updatedProduct.Availability,
                DeliverySpan = updatedProduct.DeliverySpan,
                Benefits = updatedProduct.Benefits,
                ImageUrl = updatedProduct.ImageUrl,
                IsActive = updatedProduct.IsActive,
                CreatedAt = updatedProduct.CreatedAt,
                UpdatedAt = updatedProduct.UpdatedAt
            };

            return Ok(response);
        }

        [HttpDelete("{id:int}")]
        
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {
            var isDeleted = await _productRepository.DeleteProduct(id);
            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet("Search")]
        public async Task<IActionResult> SearchProducts(string productName)
        {
            var products = await _productRepository.Searchproducts(productName);
            return Ok(products);
        }

        [HttpGet("recent-data")]
        [Authorize]
        public async Task<IActionResult> GetRecentData()
        {
            try
            {
                var users = await _usersRepository.GetRecentUsersFromSPAsync();
                var orders = await _ordersRepository.GetRecentOrdersFromSPAsync();
                var productList = await _productRepository.GetRecentProductsFromSPAsync();

                var productDetailsDict = productList.ToDictionary(p => p.ProductId, p => p);

                //var ordersDetails = orders.Select(order =>
                //{
                //    var productIds = order.productId.Split(',').Select(int.Parse);
                //    var products = productIds
                //        .Select(id => productDetailsDict.GetValueOrDefault(id))
                //        .Where(p => p != null)
                //        .ToList();

                //    return new OrdersResModel
                //    {
                //        orderId = order.orderId,
                //        userId = order.userId,
                //        productId = order.productId,
                //        addressId = order.addressId,
                //        productName = string.Join(",", products.Select(p => p.ProductName)),
                //        imageurl = string.Join(",", products.Select(p => p.ImageUrl)),
                //        DeliverySpan = string.Join(",", products.Select(p => p.DeliverySpan)),
                //        paymentMethod = order.paymentMethod,
                //        quantity = order.quantity,
                //        price = order.price,
                //        IsActive = order.IsActive,
                //        CreatedAt = order.CreatedAt,
                //        UpdatedAt = order.UpdatedAt
                //    };
                //}).ToList();

                var response = new RecentDataResponse
                {
                    Users = users.ToList(),
                    Products = productList.ToList()
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message, stackTrace = ex.StackTrace });
            }
        }



        [HttpGet("PAGEn-Data")]
        public async Task<IActionResult> GetUserDataForPageN(int pageno, int pageSize)
        {
            try
            {
                // Get paginated products data
                var productsData = await _productRepository.GetDataForPageNProduct(pageno, pageSize);

                // Get total products count
                var productsCount = await _productRepository.GetProductsCountAsync();

                // Return the result as a combined object
                return Ok(new { UserData = productsData, productCount = productsCount });
            }
            catch (Exception ex)
            {
                // Return a 500 Internal Server Error response with the exception message
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetProductsByCategoryId(int categoryId)
        {
            var products = await _productRepository.GetProductsByCategoryIdAsync(categoryId);

            if (products == null || !products.Any())
            {
                return NotFound(new { message = "No products found for this category." });
            }

            return Ok(products);
        }

        [HttpGet("statuses")]
        public async Task<IActionResult> GetProductStatuses()
        {
            var products = await _productRepository.GetProductStatusesAsync();
            return Ok(products);
        }

        [HttpPost("update-status")]
        public async Task<IActionResult> UpdateProductStatus([FromBody] ProductStatusUpdateModel request)
        {
            if (request == null)
            {
                return BadRequest("Invalid request data");
            }

            await _productRepository.UpdateProductStatusAsync(request.ProductId, request.TopSelling, request.TrendingProduct, request.RecentlyAdded);

            return Ok(new { message = "Product status updated successfully" });
        }
    }
}
