using EkartAPI.Data;
using EkartAPI.Models;
using EkartAPI.Repository.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace EkartAPI.Repository.Implementation
{
    public class OrdersRepository: IOrdersRepository
    {
        private readonly EkartDBcontext _dbContext;

        public OrdersRepository(EkartDBcontext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OrdersModel> AddOrder(OrdersModel orders)
        {
            _dbContext.Orders.Add(orders);
            await _dbContext.SaveChangesAsync();
            return orders;
        }

        public async Task<bool> DeleteOrders(int id)
        {
            var order = await _dbContext.Orders.FindAsync(id);
            if (order == null)
            {
                return false;
            }

            _dbContext.Orders.Remove(order);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<OrdersModel>> GetAllOrders()
        {
            return await _dbContext.Orders.ToListAsync();
        }

        public async Task<List<OrdersModel>> GetRecentOrdersFromSPAsync()
        {
            return await _dbContext.Orders
                                 .FromSqlRaw("EXEC Sp_GetRecentOrders")
                                 .ToListAsync();
        }

        public async Task<OrdersModel> GetOrderById(int id)
        {
            return await _dbContext.Orders.FindAsync(id);
        }

        public async Task<List<OrdersModel>> GetDataForPageNProduct(int pageno, int pageSize)
        {
            return await _dbContext.Orders
                .FromSqlRaw("exec getordersByPageNoOrdersCount {0}, {1}", pageno, pageSize)
                .ToListAsync();
        }

        public async Task<bool> UpdateOrder(OrdersModel order)
        {
            _dbContext.Orders.Update(order);
            var updated = await _dbContext.SaveChangesAsync();
            return updated > 0;
        }
        public async Task<string> UpdateOrderStatusAsync(int orderId, string newOrderStatus)
        {
            try
            {
                using (var command = _dbContext.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "UpdateOrderStatus";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@OrderId", orderId));
                    command.Parameters.Add(new SqlParameter("@NewOrderStatus", newOrderStatus));

                    if (command.Connection.State != System.Data.ConnectionState.Open)
                    {
                        await command.Connection.OpenAsync();
                    }

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.Read())
                        {
                            return reader.GetString(0); // Capturing the message from the stored procedure
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception (use a logging framework in a real application)
                Console.WriteLine(ex.Message);
                return "Error occurred while updating order status";
            }

            return "Order not found or update failed";
        }
        public async Task<string> UpdatePaymentStatusAsync(int orderId, string newPaymentStatus)
        {
            try
            {
                using (var command = _dbContext.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "UpdatePaymentStatus";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@OrderId", orderId));
                    command.Parameters.Add(new SqlParameter("@NewPaymentStatus", newPaymentStatus));

                    if (command.Connection.State != System.Data.ConnectionState.Open)
                    {
                        await command.Connection.OpenAsync();
                    }

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.Read())
                        {
                            return reader.GetString(0); // Capturing the message from the stored procedure
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception (use a logging framework in a real application)
                Console.WriteLine(ex.Message);
                return "Error occurred while updating order status";
            }

            return "Order not found or update failed";
        }


        public async Task<int> GetOrdersCountAsync()
        {
            var result = await _dbContext.Set<userCount>()
                                       .FromSqlRaw("EXEC GetOrdersRowCount")
                                       .ToListAsync();

            return result.FirstOrDefault()?.TotalRowCount ?? 0;
        }

        public async Task<string> UpdateAvailabilityAsync(int productId, int orderquantity)
        {
            try
            {
                using (var command = _dbContext.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "UpdateProductAvailability";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@ProductId", productId));
                    command.Parameters.Add(new SqlParameter("@Quantity", orderquantity));

                    if (command.Connection.State != System.Data.ConnectionState.Open)
                    {
                        await command.Connection.OpenAsync();
                    }

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.Read())
                        {
                            return reader.GetString(0); // Capturing the message from the stored procedure
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception (use a logging framework in a real application)
                Console.WriteLine(ex.Message);
                return "Error occurred while updating order status";
            }

            return "Product not found or update failed";
        }
    }
}
