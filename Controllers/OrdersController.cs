using EkartAPI.Migrations;
using EkartAPI.Models;
using EkartAPI.Repository.Implementation;
using EkartAPI.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Razorpay.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using EkartAPI.Models.ResponseModels;

namespace EkartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IProductRepository _productRepository;
        private readonly IDeliveryAddressRepository _deliveryRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IConfiguration _configuration;

        public OrdersController(IOrdersRepository ordersRepository, IProductRepository productRepository, ICartRepository cartRepository, IDeliveryAddressRepository deliveryRepository, IConfiguration configuration)
        {
            _ordersRepository = ordersRepository;
            _productRepository = productRepository;
            _cartRepository = cartRepository;
            _deliveryRepository = deliveryRepository;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _ordersRepository.GetAllOrders();
            var response = new List<ResentOrdersModel>();

            foreach (var order in orders)
            {
                var productIds = order.productId.Split(',')
                                                .Select(id => int.TryParse(id.Trim(), out var intId) ? intId : (int?)null)
                                                .Where(id => id.HasValue)
                                                .Select(id => id.Value)
                                                .ToList();

                var products = new List<Product>();

                foreach (var productId in productIds)
                {
                    var product = await _productRepository.GetProductById(productId);
                    if (product != null)
                    {
                        products.Add(product);
                    }
                }

                var productNames = string.Join(", ", products.Select(p => p.ProductName));
                var imageUrls = string.Join(", ", products.Select(p => p.ImageUrl));
                var deliverySpans = string.Join(", ", products.Select(p => p.DeliverySpan));
                var deliveryAddress = await _deliveryRepository.GetDeliveryAddressById(order.addressId);

                response.Add(new ResentOrdersModel
                {
                    orderId = order.orderId,
                    userId = order.userId,
                    productId = order.productId,
                    addressId = order.addressId,
                    productName = productNames,
                    imageurl = imageUrls,
                    DeliverySpan = deliverySpans,
                    PaymentStatus = order.PaymentStatus,
                    OrderStatus = order.OrderStatus,
                    FullName = deliveryAddress.FullName,
                    MobileNumber = deliveryAddress.MobileNumber,
                    AlternateMobileNumber = deliveryAddress.AlternateMobileNumber,
                    PinCode = deliveryAddress.PinCode,
                    HouseNo = deliveryAddress.HouseNo,
                    AreaDetails = deliveryAddress.AreaDetails,
                    Landmark = deliveryAddress.Landmark,
                    City = deliveryAddress.City,
                    State = deliveryAddress.State,
                    TypeOfAddress = deliveryAddress.TypeOfAddress,
                    paymentMethod = order.paymentMethod,
                    price = order.price,
                    quantity = order.quantity,
                    IsActive = order.IsActive,
                    CreatedAt = order.CreatedAt,
                    UpdatedAt = order.UpdatedAt
                });
            }

            // Sort the response by orderId in descending order
            var sortedResponse = response.OrderByDescending(o => o.orderId).ToList();

            return Ok(sortedResponse);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOrderById([FromRoute] int id)
        {
            var order = await _ordersRepository.GetOrderById(id);

            if (order == null)
            {
                return NotFound();
            }

            var productIds = order.productId.Split(',')
                                            .Select(id => int.TryParse(id.Trim(), out var intId) ? intId : (int?)null)
                                            .Where(id => id.HasValue)
                                            .Select(id => id.Value)
                                            .ToList();

            var products = new List<Product>();

            foreach (var productId in productIds)
            {
                var product = await _productRepository.GetProductById(productId);
                if (product != null)
                {
                    products.Add(product);
                }
            }

            var productNames = string.Join(", ", products.Select(p => p.ProductName));
            var imageUrls = string.Join(", ", products.Select(p => p.ImageUrl));
            var deliverySpans = string.Join(", ", products.Select(p => p.DeliverySpan));

            var response = new OrdersResModel
            {
                orderId = order.orderId,
                userId = order.userId,
                productId = order.productId,
                addressId = order.addressId,
                productName = productNames,
                imageurl = imageUrls,
                DeliverySpan = deliverySpans,
                paymentMethod = order.paymentMethod,
                PaymentStatus = order.PaymentStatus,
                OrderStatus = order.OrderStatus,
                price = order.price,
                quantity = order.quantity,
                IsActive = order.IsActive,
                CreatedAt = order.CreatedAt,
                UpdatedAt = order.UpdatedAt
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrders([FromBody] PostOrdersModel order)
        {
            var orders = new OrdersModel
            {
                userId = order.userId,
                productId = order.productId, // Assuming you're storing the first productId; update logic might be needed
                addressId = order.addressId,
                paymentMethod = order.paymentMethod,
                PaymentStatus = order.PaymentStatus,
                OrderStatus = order.OrderStatus,
                price = order.price, // Assuming you're storing the price of the first product; update logic might be needed
                quantity = order.quantity, // Assuming you're storing the quantity of the first product; update logic might be needed
                IsActive = order.IsActive,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            // Parse productId and quantity strings
            string[] productIds = order.productId.Split(',');
            string[] quantities = order.quantity.Split(',');

            if (productIds.Length != quantities.Length)
            {
                return BadRequest("The number of product IDs and quantities must match.");
            }

            List<Task> updateTasks = new List<Task>();

            for ( 
                int i = 0; i < productIds.Length; i++)
            {
                if (int.TryParse(productIds[i], out int parsedProductId) &&
                    int.TryParse(quantities[i], out int parsedQuantity))
                {
                    updateTasks.Add(_ordersRepository.UpdateAvailabilityAsync(parsedProductId, parsedQuantity));
                    await Task.Delay(100);
                }
                else
                {
                    Console.WriteLine($"Invalid product ID or quantity: {productIds[i]}, {quantities[i]}");
                }
            }

            await Task.WhenAll(updateTasks);

            var addedorder = await _ordersRepository.AddOrder(orders);
            string cartIds = order.cartId;

            string[] cartIdArray = cartIds.Split(',');

            List<Task> deleteTasks = new List<Task>();

            foreach (string cartId in cartIdArray)
            {
                if (int.TryParse(cartId, out int parsedCartId))
                {
                    deleteTasks.Add(_cartRepository.DeleteCartItemAsync(parsedCartId));
                    await Task.Delay(100);
                }
                else
                {
                    Console.WriteLine($"Invalid cart ID: {cartId}");
                }
            }

            await Task.WhenAll(deleteTasks);

            var response = new OrdersModel
            {
                orderId = addedorder.orderId,
                userId = addedorder.userId,
                productId = addedorder.productId,
                addressId = addedorder.addressId,
                paymentMethod = addedorder.paymentMethod,
                price = addedorder.price,
                quantity = addedorder.quantity,
                IsActive = addedorder.IsActive,
                CreatedAt = addedorder.CreatedAt,
                UpdatedAt = addedorder.UpdatedAt
            };

            return Ok(response);
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOrder([FromRoute] int id)
        {
            var isDeleted = await _ordersRepository.DeleteOrders(id);
            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet("PAGEn-Data")]
        public async Task<IActionResult> GetOrdersDataForPageN(int pageno, int pageSize)

        {
            try
            {
                int orderspageNo = pageno;
                var ordersData = await _ordersRepository.GetDataForPageNProduct(orderspageNo, pageSize);

                var ordersCount = await _ordersRepository.GetOrdersCountAsync();
                var response = new List<OrdersResModel>();

                foreach (var order in ordersData)
                {
                    var productIds = order.productId.Split(',')
                                                    .Select(id => int.TryParse(id.Trim(), out var intId) ? intId : (int?)null)
                                                    .Where(id => id.HasValue)
                                                    .Select(id => id.Value)
                                                    .ToList();
                    var products = new List<Product>();

                    foreach (var productId in productIds)
                    {
                        var product = await _productRepository.GetProductById(productId);
                        if (product != null)
                        {
                            products.Add(product);
                        }
                    }

                    var productNames = string.Join(", ", products.Select(p => p.ProductName));
                    var imageUrls = string.Join(", ", products.Select(p => p.ImageUrl));
                    var deliverySpans = string.Join(", ", products.Select(p => p.DeliverySpan));
                    response.Add(new OrdersResModel
                    {
                        orderId = order.orderId,
                        userId = order.userId,
                        productId = order.productId,
                        addressId = order.addressId,
                        productName = productNames,
                        imageurl = imageUrls,
                        DeliverySpan = deliverySpans,
                        paymentMethod = order.paymentMethod,
                        PaymentStatus = order.PaymentStatus,
                        OrderStatus = order.OrderStatus,
                        price = order.price,
                        quantity = order.quantity,
                        IsActive = order.IsActive,
                        CreatedAt = order.CreatedAt,
                        UpdatedAt = order.UpdatedAt
                    });
                }

                return Ok(new { OrderData = response, OrderCount = ordersCount });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }


        //[HttpPost("initiate-payment")]
        //public IActionResult InitiatePayment([FromBody] PaymentRequestModel paymentRequest)
        //{
        //    try
        //    {
        //        var razorpayKey = _configuration["Razorpay:Key"];
        //        var razorpaySecret = _configuration["Razorpay:Secret"];

        //        // Initialize Razorpay client
        //        var client = new RazorpayClient(razorpayKey, razorpaySecret);

        //        // Razorpay order options
        //        var options = new Dictionary<string, object>
        //{
        //    { "amount", paymentRequest.Amount * 100 },  // Amount should be in paise (100 paise = 1 INR)
        //    { "currency", "INR" },
        //    { "receipt", Guid.NewGuid().ToString() },
        //    { "payment_capture", 1 }
        //};

        //        // Create Razorpay order
        //        var order = client.Order.Create(options);

        //        // Return Razorpay order ID and other details
        //        var razorpayOrderId = order["id"].ToString();

        //        return Ok(new
        //        {
        //            OrderId = razorpayOrderId,
        //            RazorpayKey = razorpayKey,
        //            Amount = paymentRequest.Amount
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while initiating the payment", error = ex.Message });
        //    }
        //}


        //[HttpPost("verify-payment")]
        //public async Task<IActionResult> VerifyPayment([FromBody] PaymentVerificationModel verificationModel)
        //{
        //    try
        //    {
        //        var razorpayKey = _configuration["Razorpay:Key"];
        //        var razorpaySecret = _configuration["Razorpay:Secret"];

        //        var client = new RazorpayClient(razorpayKey, razorpaySecret);
        //        var attributes = new Dictionary<string, string>
        //        {
        //            { "razorpay_order_id", verificationModel.razorpay_order_id },
        //            { "razorpay_payment_id", verificationModel.razorpay_payment_id },
        //            { "razorpay_signature", verificationModel.razorpay_signature }
        //        };

        //        Utils.verifyPaymentSignature(attributes);

        //        // Uncomment and update this section to handle order status in your database
        //        // var order = await _ordersRepository.GetOrderById(verificationModel.razorpay_order_id);
        //        // if (order != null)
        //        // {
        //        //     order.IsActive = true;
        //        //     await _ordersRepository.UpdateOrder(order);
        //        // }

        //        return Ok(new { success = true, message = "Payment verification successful" });
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the error
        //        Console.WriteLine($"Payment verification failed: {ex.Message}");
        //        return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Payment verification failed", error = ex.Message });
        //    }
        //}





        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateOrderStatus(int id, [FromBody] UpdateStatusRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Status))
            {
                return BadRequest(new { Message = "New order status cannot be empty" });
            }

            var resultMessage = await _ordersRepository.UpdateOrderStatusAsync(id, request.Status);
            var orders = await _ordersRepository.GetOrderById(id);
            if (orders.paymentMethod == "Cash on Delivery" && orders.OrderStatus == "Delivered")
            {
               await _ordersRepository.UpdatePaymentStatusAsync(id, "Success");
            }


            if (resultMessage.Contains("Order status updated successfully"))
            {
                return Ok(new { Message = resultMessage });
            }

            return NotFound(new { Message = resultMessage });
        }



        [HttpPut("{id}/paymentstatus")]
        public async Task<IActionResult> UpdatePaymentStatus(int id, [FromBody] UpdatePaymentStatusRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.PaymentStatus))
            {
                return BadRequest(new { Message = "New Payment status cannot be empty" });
            }

            var resultMessage = await _ordersRepository.UpdatePaymentStatusAsync(id, request.PaymentStatus);

            if (resultMessage.Contains("Payment status updated successfully"))
            {
                return Ok(new { Message = resultMessage });
            }

            return NotFound(new { Message = resultMessage });
        }


    }
}
