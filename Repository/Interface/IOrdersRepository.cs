

using EkartAPI.Models;

namespace EkartAPI.Repository.Interface
{
    public interface IOrdersRepository
    {
        Task<IEnumerable<OrdersModel>> GetAllOrders();
        Task<List<OrdersModel>> GetRecentOrdersFromSPAsync();
        Task<OrdersModel> GetOrderById(int id);
        Task<OrdersModel> AddOrder(OrdersModel orders);
        Task<bool> DeleteOrders(int id);
        Task<List<OrdersModel>> GetDataForPageNProduct(int pageno, int pageSize);
        Task<bool> UpdateOrder(OrdersModel order);
        Task<string> UpdateOrderStatusAsync(int orderId, string newOrderStatus);
        Task<string> UpdatePaymentStatusAsync(int orderId, string newPaymentStatus);
        Task<string> UpdateAvailabilityAsync(int productId, int orderquantity);
        Task<int> GetOrdersCountAsync();
    }
}
