using EkartAPI.Models;
using EkartAPI.Models.ResponseModels;

namespace EkartAPI.Repository.Interface
{
    public interface IOrdercheckout
    {
        public Task<OrdersCheckout> PlaceOrderWithoutSP(OrdersCheckout order);
        Task<OrderFKResponseModel> GetOrdersByUserId(int userId);
        Task<OrderFKResponseModel> GetAllOrders();
        Task<OrderCancelledFKResponseModel> GetCancelledOrders();

        Task<ShippingAddress> getshippingaddressbyorderId(int userId);
        Task AddOrderDeliveryDetailAsync(OrderDeliveryDetail detail);
        Task<OrderDetailsDTO> GetOrderDetailsByOrderIdAsync(int orderId);
        Task<OrderRefundDetailsDTO> GetOrderRefundDetailsByOrderIdAsync(int orderId);

        Task<bool> UpdateAsync(UpdateOrderStatus orderStatus);
        Task<OrderDeliveryDetail> GetDeliveryDetailsByOrderIdAsync(int orderId);
        Task EditOrderDeliveryDetailAsync(OrderDeliveryDetail detail);
        Task CancelOrderAsync(CancelOrderRequest request);

        // Update the order status to "Failed" using a stored procedure
        Task<OrderDetails> UpdateOrderStatusAsync(string razorpayOrderId, string failureReason);
        public  Task<OrderDetails> GetOrderByRazorpayOrderIdAsync(string razorpayOrderId);
        Task<bool> UpdateOrderAsync(UpdateOrdersCheckout updatedOrder);
        Task<bool> UpdateRefundStatusAsync(int orderId, string status);



    }
}
