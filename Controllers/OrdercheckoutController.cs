using cashfree_pg.Client;
using cashfree_pg.Model;
using EkartAPI.Data;
using EkartAPI.Models;
using EkartAPI.Models.ResponseModels;
using EkartAPI.Models.Test;
using EkartAPI.Repository.Implementation;
using EkartAPI.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Ocsp;
using Razorpay.Api;
using System.Data;
using System.Globalization;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EkartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdercheckoutController : ControllerBase
    {
        private readonly EkartDBcontext _dbContext;
        private readonly ILogger<OrdercheckoutController> _logger;
        private readonly IOrdercheckout _orderRepository;
        private readonly IConfiguration _configuration;
        private readonly IEmailInterface _emailRepository;
        private readonly ISmsRepository _smsRepository;
        private readonly IUserRepository _userRepository;
        private readonly HttpClient _httpClient;
        private readonly CashfreeSettings _cashfreeSettings;
        private readonly IWhatsAppRepository _whatsAppRepository;



        // Fix for CS7036: Ensure the required parameter 'XEnvironment' is passed to the Cashfree constructor.

        public OrdercheckoutController(EkartDBcontext dbContext, IEmailInterface emailInterface, ILogger<OrdercheckoutController> logger, IOrdercheckout orderRepository, IConfiguration configuration,
            ISmsRepository smsRepository, IOptions<CashfreeSettings> cashfreeSettings, IUserRepository userRepository, IWhatsAppRepository whatsAppRepository)
        {
            _logger = logger;
            _orderRepository = orderRepository;
            _configuration = configuration;
            _emailRepository = emailInterface;
            _dbContext = dbContext;
            _smsRepository = smsRepository;
            _userRepository = userRepository;
            _httpClient = new HttpClient();
            _cashfreeSettings = cashfreeSettings.Value;

            // Correctly initialize the Cashfree instance with the required parameters.
            var cashfreeClient = new Cashfree(
                _cashfreeSettings.Environment.ToUpper() == "SANDBOX" ? CFEnvironment.PRODUCTION : CFEnvironment.SANDBOX,
                _cashfreeSettings.ClientId,
                _cashfreeSettings.ClientSecret,
                null, // Assuming XPartnerApiKey is not required, pass null or appropriate value
                null, // Assuming XPartnerMerchantId is not required, pass null or appropriate value
                null, // Assuming XClientSignature is not required, pass null or appropriate value
                false // Assuming XEnableErrorAnalytics is false, adjust as needed
            );
            _userRepository = userRepository;
            _whatsAppRepository = whatsAppRepository;

            // Use the instance of Cashfree for further operations
        }
        [HttpPost("place")]
        [Authorize]
        public async Task<IActionResult> PlaceOrder([FromBody] OrdersCheckout order)
        {
            try
            {
                if (order == null || order.ProductDetails == null || order.ProductDetails.Count == 0)
                    return BadRequest("Invalid order request. Product details are required.");

                var createdOrder = await _orderRepository.PlaceOrderWithoutSP(order);

                return CreatedAtAction(nameof(PlaceOrder), new { id = createdOrder.userId }, createdOrder);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error placing order");
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    message = "Error placing order",
                    error = ex.Message,
                    inner = ex.InnerException?.Message,
                    stack = ex.StackTrace,
                    full = ex.ToString()
                });
            }
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetOrdersByUserId()
        {
            var userIdClaim = User.FindFirst("userId");

            if (!int.TryParse(userIdClaim.Value, out int userId))
            {
                return BadRequest(new { message = "Invalid User ID format" });
            }
            var result = await _orderRepository.GetOrdersByUserId(userId);

            if (result == null || result.data.Count == 0)
            {
                return NotFound(new { message = "No orders found for this user." });
            }

            return Ok(result);
        }

        [HttpGet("order-details/{orderId}")]
        public async Task<IActionResult> GetOrderDetails(int orderId)
        {
            var result = await _orderRepository.GetOrderDetailsByOrderIdAsync(orderId);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("order-refunddetails/{orderId}")]
        public async Task<IActionResult> GetOrderRefundDetails(int orderId)
        {
            var result = await _orderRepository.GetOrderRefundDetailsByOrderIdAsync(orderId);
            if (result == null)
                return NotFound();

            return Ok(result);
        }



        [HttpGet("all-orders")]

        public async Task<IActionResult> GetAllOrders()
        {
            var result = await _orderRepository.GetAllOrders();

            if (result == null || result.data.Count == 0)
            {
                return NotFound(new { message = "No orders found." });
            }

            return Ok(result);
        }

        [HttpGet("cancelled-orders")]

        public async Task<IActionResult> GetCancelledOrders()
        {
            var result = await _orderRepository.GetCancelledOrders();

            if (result == null || result.data.Count == 0)
            {
                return NotFound(new { message = "No orders found." });
            }

            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostOrderDeliveryDetail([FromBody] OrderDeliveryDetail detail)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _orderRepository.AddOrderDeliveryDetailAsync(detail);
            return Ok(new { message = "Dispatched order delivery detail saved successfully!" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderStatus(int id, [FromBody] UpdateOrderStatus model)
        {
            var updated = await _orderRepository.UpdateAsync(model);
            if (!updated)
                return NotFound("Order status not found");

            //return Ok("Order status updated successfully");

            return Ok(new OrderDetailsDTO());
        }

        [HttpGet("GetByOrderId/{orderId}")]
        [Authorize]
        public async Task<IActionResult> GetDeliveryDetailsByOrderId(int orderId)
        {
            var details = await _orderRepository.GetDeliveryDetailsByOrderIdAsync(orderId);
            if (details == null)
            {
                return NotFound(new { Message = "Delivery details not found for the given OrderId." });
            }
            return Ok(details);
        }

        //[HttpPut("{orderId}")]
        //public async Task<IActionResult> EditOrderDeliveryDetail(int orderId, [FromBody] OrderDeliveryDetail updatedDetail)
        //{
        //    if (updatedDetail == null)
        //    {
        //        return BadRequest(new { message = "Invalid input data." });
        //    }

        //    updatedDetail.OrderId = orderId; // Ensure the OrderId is set

        //    try
        //    {
        //        await _orderRepository.EditOrderDeliveryDetailAsync(updatedDetail);
        //        return Ok(new { message = "Order delivery detail updated successfully!" });
        //    }
        //    catch (KeyNotFoundException ex)
        //    {
        //        return NotFound(new { message = ex.Message });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { message = "An error occurred while updating delivery details.", error = ex.Message });
        //    }
        //}

        [HttpPost("cancel")]
        [Authorize]
        public async Task<IActionResult> CancelOrder([FromBody] CancelOrderRequest request)
        {
            await _orderRepository.CancelOrderAsync(request);
            return Ok(new { message = "Order canceled successfully." });
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> CashfreeWebhook([FromBody] dynamic payload)
        {
            string json = JsonConvert.SerializeObject(payload);
            _logger.LogInformation("Cashfree webhook: " + json);

            string orderId = payload.order_id;
            string status = payload.order_status;

            if (status == "PAID")
            {
                await _orderRepository.UpdateOrderStatusAsync(orderId, "Success");
            }
            else
            {
                await _orderRepository.UpdateOrderStatusAsync(orderId, "Failed");
            }

            return Ok();
        }



        [HttpPost("mark-payment-failed")]
        [Authorize]
        public async Task<IActionResult> MarkPaymentFailed([FromBody] PaymentFailureModel failureModel)
        {
            if (string.IsNullOrEmpty(failureModel.razorpay_order_id))
                return BadRequest(new { message = "Missing Razorpay Order ID" });

            try
            {
                _logger.LogInformation($"Received Razorpay Order ID: {failureModel.razorpay_order_id}");

                // Check if the order exists using the stored procedure (no entity needed)
                var order = await _orderRepository.GetOrderByRazorpayOrderIdAsync(failureModel.razorpay_order_id);
                if (order == null)
                {
                    _logger.LogWarning($"Order not found for Razorpay Order ID: {failureModel.razorpay_order_id}");
                    return NotFound(new { message = "Order not found for the given Razorpay Order ID" });
                }

                // Mark order as failed (update status via stored procedure)
                var updatedOrder = await _orderRepository.UpdateOrderStatusAsync(failureModel.razorpay_order_id, failureModel.reason);

                return Ok(new { message = "Payment marked as failed successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error processing payment failure: {ex.Message}");
                return StatusCode(500, new { message = "Error while marking payment as failed", error = ex.Message });
            }
        }
        [HttpPost("initiate-payment")]
        [Authorize]
        public async Task<IActionResult> InitiatePayment([FromBody] PaymentRequestModel request)
        {
            var userIdClaim = User.FindFirst("userId");

            if (!int.TryParse(userIdClaim.Value, out int userId))
            {
                return BadRequest(new { message = "Invalid User ID format" });
            }

            var userDetails = await _userRepository.GetUserByIdAsync(userId);
            try
            {
                var cashfree = new Cashfree(
    _cashfreeSettings.Environment.ToUpper() == "SANDBOX"
        ? CFEnvironment.PRODUCTION
        : CFEnvironment.SANDBOX,
    _cashfreeSettings.ClientId,
    _cashfreeSettings.ClientSecret,
    null,
    null,
    null,
    false
);


                var createOrderRequest = new CreateOrderRequest(
                    orderId: Guid.NewGuid().ToString(),   // unique
                    orderAmount: (double)request.Amount,  // double with decimals
                    orderCurrency: "INR",
                    customerDetails: new CustomerDetails(
                        customerId: request.OrderId.ToString(),
                        customerEmail: userDetails.Email,
                        customerPhone: userDetails.MobileNumber
                    )
                );

                var result = cashfree.PGCreateOrder(createOrderRequest);

                return Ok(new
                {
                    Status = result.StatusCode,
                    Order = result.Content
                });
            }
            catch (ApiException e)
            {
                return StatusCode(500, new
                {
                    message = "Error initiating Cashfree payment",
                    error = e.Message,
                    statusCode = e.ErrorCode
                });
            }
        }


        [HttpPost("mark-payment-success")]
        [Authorize]
        public async Task<IActionResult> MarkPaymentSuccess([FromBody] MarkPaymentSuccessModel model)
        {
            try
            {
                using var connection = _dbContext.Database.GetDbConnection();
                await connection.OpenAsync();

                using var command = connection.CreateCommand();
                command.CommandText = "UpdateOrderStatusByRazorpayOrderId";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@RazorpayOrderId", model.RazorpayOrderId));
                command.Parameters.Add(new SqlParameter("@Status", model.Status));

                var rowsAffectedParam = new SqlParameter("@RowsAffected", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(rowsAffectedParam);

                await command.ExecuteNonQueryAsync();
                int rowsAffected = (int)(rowsAffectedParam.Value ?? 0);

                if (rowsAffected <= 0)
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "Order not found for given RazorpayOrderId"
                    });
                }

                var getOrderCmd = connection.CreateCommand();
                getOrderCmd.CommandText = @"
            SELECT o.OrderId, o.TotalPrice, o.UserID,
                   u.FirstName, u.LastName, u.MobileNumber
            FROM OrderDetails o
            JOIN tbl_users u ON o.UserID = u.UserId
            WHERE RTRIM(LTRIM(o.RazorpayOrderId)) = RTRIM(LTRIM(@RazorpayOrderId))
        ";

                getOrderCmd.Parameters.Add(
                    new SqlParameter("@RazorpayOrderId", model.RazorpayOrderId));

                using var reader = await getOrderCmd.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    var orderId = reader["OrderId"].ToString();
                    var firstName = reader["FirstName"].ToString();
                    var lastName = reader["LastName"].ToString();
                    var mobile = reader["MobileNumber"].ToString();

                    var fullName = $"{firstName} {lastName}";
                    var orderDate = DateTime.Now.ToString("dd MMM yyyy");

                    // ✅ Send WhatsApp Order Confirmation
                    await _whatsAppRepository.SendOrderConfirmationAsync(
                        mobile,
                        fullName,
                        orderId,
                        orderDate
                    );
                }

                return Ok(new
                {
                    success = true,
                    message = "Order marked as Success. WhatsApp notification sent."
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, new
                {
                    success = false,
                    message = "Error updating order status"
                });
            }
        }


        [HttpPost("update-razorpay-order-id")]
        [Authorize]
        public async Task<IActionResult> UpdateRazorpayOrderId([FromBody] UpdateRazorpayOrderIdModel model)
        {
            Console.WriteLine($"Attempting to update RazorpayOrderId: {model.OrderId} => {model.RazorpayOrderId}");

            try
            {
                using var connection = _dbContext.Database.GetDbConnection();
                await connection.OpenAsync();

                using var command = connection.CreateCommand();
                command.CommandText = "UpdateRazorpayOrderIdByOrderId";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@OrderId", model.OrderId));
                command.Parameters.Add(new SqlParameter("@RazorpayOrderId", model.RazorpayOrderId));

                var rowsAffectedParam = new SqlParameter("@RowsAffected", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(rowsAffectedParam);

                await command.ExecuteNonQueryAsync();

                int rowsAffected = (int)(rowsAffectedParam.Value ?? 0);

                if (rowsAffected > 0)
                {
                    return Ok(new { success = true, message = "RazorpayOrderId updated successfully" });
                }
                else
                {
                    Console.WriteLine($"❌ Order not found for OrderId: {model.OrderId}");
                    return NotFound(new { success = false, message = "Order not found" });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
                return StatusCode(500, new { success = false, message = "Error updating RazorpayOrderId" });
            }
        }
        [HttpPost("retry-payment")]
        [Authorize]
        public async Task<IActionResult> RetryPayment([FromBody] RetryPaymentRequest request)
        {
            try
            {
                // Fetch the existing order
                var existingOrder = await _dbContext.OrderDetails.FindAsync(request.OrderId);
                if (existingOrder == null)
                    return NotFound(new { message = "Order not found" });

                // Check if already paid
                if (existingOrder.Status == "Success")
                    return Conflict(new { message = "Order already paid" });

                // Fetch user details
                var ordereduser = await _userRepository.GetUserByIdAsync(existingOrder.UserID);
                if (ordereduser == null)
                    return NotFound(new { message = "User not found" });

                // Fetch shipping address
                var shippingaddressDetails = await _orderRepository.getshippingaddressbyorderId(request.OrderId);
                if (shippingaddressDetails == null)
                    return NotFound(new { message = "Shipping address not found" });

                // Create Cashfree order
                var createOrderRequest = new CreateOrderRequest(
                    orderId: Guid.NewGuid().ToString(),
                    orderAmount: (double)existingOrder.TotalPrice,
                    orderCurrency: "INR",
                    customerDetails: new CustomerDetails(
                        customerId: existingOrder.UserID.ToString(),
                        customerEmail: ordereduser.Email,
                        customerPhone: shippingaddressDetails.MobileNumber
                    )
                );

                var cashfree = new Cashfree(
                    CFEnvironment.SANDBOX,
                    _cashfreeSettings.ClientId,
                    _cashfreeSettings.ClientSecret,
                    null, null, null, false
                );

                var result = cashfree.PGCreateOrder(createOrderRequest);
                if (result.StatusCode != System.Net.HttpStatusCode.OK || result.Content == null)
                    return StatusCode(500, new { message = "Failed to create Cashfree order" });

                var json = Newtonsoft.Json.JsonConvert.SerializeObject(result.Content);
                var orderResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<CashfreeOrderResponse>(json);

                // Update order in DB
                existingOrder.RazorpayOrderId = orderResponse.CfOrderId;
                existingOrder.Status = "Pending";
                await _dbContext.SaveChangesAsync();

                // Return response with top-level paymentSessionId for frontend
                return Ok(new
                {
                    status = 200,
                    cfOrderId = orderResponse.CfOrderId,
                    paymentSessionId = orderResponse.PaymentSessionId, // ⚡ important
                    order = orderResponse
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Unexpected error", error = ex.Message });
            }
        }



        [HttpGet("verify-payment")]
        [Authorize]
        public async Task<IActionResult> VerifyPayment(string orderId)
        {
            var clientId = _cashfreeSettings.ClientId;
            var clientSecret = _cashfreeSettings.ClientSecret;
            var apiUrl = $"https://api.cashfree.com/pg/orders/{orderId}";

            using var client = new HttpClient();
            var authBytes = Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}");
            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(authBytes));

            var response = await client.GetAsync(apiUrl);
            var responseString = await response.Content.ReadAsStringAsync();

            return Ok(JsonConvert.DeserializeObject<object>(responseString));
        }
        [HttpPut("update-order")]
        [Authorize]
        public async Task<IActionResult> UpdateOrder([FromBody] UpdateOrdersCheckout updatedOrder)
        {
            if (updatedOrder == null || updatedOrder.OrderId <= 0)
                return BadRequest(new { message = "Invalid order update request." });

            try
            {
                var result = await _orderRepository.UpdateOrderAsync(updatedOrder);
                if (result)
                {
                    return Ok(new { message = "Order updated successfully." });
                }
                else
                {
                    return NotFound(new { message = "Order not found." });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating order {updatedOrder.OrderId}: {ex.Message}");
                return StatusCode(500, new { message = "Error updating order.", error = ex.Message });
            }
        }

        [HttpPut("update-refundstatus")]
        public async Task<IActionResult> UpdateRefundStatus(
            [FromBody] UpdateRefundStatusRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _orderRepository
                .UpdateRefundStatusAsync(request.OrderId, request.Status);

            if (!result)
                return NotFound(new
                {
                    message = $"Refund record not found for OrderId {request.OrderId}"
                });

            return Ok(new
            {
                message = "Refund status updated successfully"
            });
        }

       

            [HttpPost("test")]
            public async Task<IActionResult> TestWhatsApp()
            {
                try
                {
                    await _whatsAppRepository.SendOrderConfirmationAsync(
                        "916302594338",          // test number
                        "Test User",
                        "TEST-ORDER-001",
                        DateTime.Now.ToString("dd MMM yyyy")
                    );

                    return Ok(new
                    {
                        success = true,
                        message = "WhatsApp API working successfully"
                    });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new
                    {
                        success = false,
                        message = "WhatsApp API test failed",
                        error = ex.Message
                    });
                }
            }
  

    }
}
