using EkartAPI.Models.ResponseModels;
using EkartAPI.Repository.Interface;
using EkartAPI.Data;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Globalization;
using System.Net.NetworkInformation;
using static EkartAPI.Repository.Implementation.OrdercheckoutRepository;
using Org.BouncyCastle.Asn1.X9;
using EkartAPI.Models;

namespace EkartAPI.Repository.Implementation
{
    public class OrdercheckoutRepository : IOrdercheckout
    {
        private readonly EkartDBcontext _dbContext;
        private readonly IEmailInterface _emailRepository;

        public OrdercheckoutRepository(EkartDBcontext dbContext, IEmailInterface emailInterface)
        {
            _dbContext = dbContext;
            _emailRepository = emailInterface;
        }

        public async Task<OrdersCheckout> PlaceOrderWithoutSP(OrdersCheckout order)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                // 1. Insert into OrderDetails
                var orderEntity = new OrderDetails
                {
                    UserID = order.userId,
                    DeliveryDescription = order.DeliveryDescription,
                    PaymentMethod = order.PaymentMethod,
                    TotalPrice = order.Totalprices,
                    Status = "Pending",
                    CreatedAt = DateTime.UtcNow,
                    RazorpayOrderId = order.RazorpayOrderId,
                    FailureReason = order.FailureReason
                };
                _dbContext.OrderDetails.Add(orderEntity);
                await _dbContext.SaveChangesAsync();

                // 2. Insert products into OrdersProduct
                var products = await _dbContext.tbl_products
                    .Where(p => order.ProductDetails.Select(x => x.ProductId).Contains(p.ProductId))
                    .ToListAsync();

                foreach (var item in order.ProductDetails)
                {
                    var product = products.First(p => p.ProductId == item.ProductId);

                    _dbContext.OrdersProduct.Add(new OrdersProduct
                    {
                        OrderId = orderEntity.OrderId,
                        ProductId = product.ProductId,
                        ProductName = product.ProductName,
                        Description = product.Description,
                        Quantity = item.quantity,
                        Price = product.Price,
                        ImageUrl = product.ImageUrl
                    });
                }

                await _dbContext.SaveChangesAsync();


                // 3. Insert shipping address
                var shipping = await _dbContext.tbl_DeliveryAddress
                    .FirstOrDefaultAsync(a => a.AddId == order.AddressId);

                if (shipping != null)
                {
                    _dbContext.ShippingAddress.Add(new ShippingAddress
                    {
                        OrderID = orderEntity.OrderId,
                        FullName = shipping.FullName,
                        MobileNumber = shipping.MobileNumber,
                        AlternateMobileNumber = shipping.AlternateMobileNumber,
                        PinCode = shipping.PinCode,
                        HouseNo = shipping.HouseNo,
                        AreaDetails = shipping.AreaDetails,
                        Landmark = shipping.Landmark,
                        City = shipping.City,
                        State = shipping.State,
                        TypeOfAddress = shipping.TypeOfAddress,
                        IsActive = true
                    });
                }
                await _dbContext.SaveChangesAsync();

                // 4. Insert status
                _dbContext.OrderStatus.Add(new OrderStatus
                {
                    OrderId = orderEntity.OrderId,
                    name = "Pending",
                    Slug = "Pending",
                    sequence = "1",
                    status = true,
                    created_At = DateTime.UtcNow,
                    updated_At = DateTime.UtcNow
                });
                await _dbContext.SaveChangesAsync();

                // Commit transaction
                await transaction.CommitAsync();

                // Map back to DTO
                order.OrderId = orderEntity.OrderId;
                return order;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> UpdateOrderAsync(UpdateOrdersCheckout updatedOrder)
        {
            using var connection = _dbContext.Database.GetDbConnection();
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "UpdateOrderDetails"; // Your stored procedure to update order details
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("@OrderId", updatedOrder.OrderId));
            command.Parameters.Add(new SqlParameter("@UserId", updatedOrder.UserId));
            command.Parameters.Add(new SqlParameter("@AddressId", updatedOrder.AddressId));
            command.Parameters.Add(new SqlParameter("@DeliveryDescription", updatedOrder.DeliveryDescription));
            command.Parameters.Add(new SqlParameter("@PaymentMethod", updatedOrder.PaymentMethod));
            command.Parameters.Add(new SqlParameter("@Totalprices", updatedOrder.Totalprices));
            command.Parameters.Add(new SqlParameter("@Status", updatedOrder.Status));
            command.Parameters.Add(new SqlParameter("@RazorpayOrderId", updatedOrder.RazorpayOrderId ?? ""));
            command.Parameters.Add(new SqlParameter("@FailureReason", updatedOrder.FailureReason ?? ""));

            // If you also want to update product details, you might have to pass a table-valued parameter similar to place order

            var rowsAffectedParam = new SqlParameter("@RowsAffected", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            command.Parameters.Add(rowsAffectedParam);

            await command.ExecuteNonQueryAsync();

            int rowsAffected = (int)(rowsAffectedParam.Value ?? 0);
            return rowsAffected > 0;
        }
        public async Task<OrderFKResponseModel> GetOrdersByUserId(int userId)
        {
            // --- Helpers ---
            int? GetNullableInt(SqlDataReader r, string col)
            {
                int ord = r.GetOrdinal(col);
                return r.IsDBNull(ord) ? (int?)null : r.GetInt32(ord);
            }

            decimal? GetNullableDecimal(SqlDataReader r, string col)
            {
                int ord = r.GetOrdinal(col);
                return r.IsDBNull(ord) ? (decimal?)null : r.GetDecimal(ord);
            }

            string GetNullableString(SqlDataReader r, string col)
            {
                int ord = r.GetOrdinal(col);
                return r.IsDBNull(ord) ? null : r.GetString(ord);
            }

            var orderResponse = new OrderFKResponseModel();
            var orders = new List<OrderFK>();

            using (var connection = (SqlConnection)_dbContext.Database.GetDbConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("GetOrdersByUserId", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = userId });

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        // 1. Read Orders
                        while (await reader.ReadAsync())
                        {
                            var order = new OrderFK
                            {
                                order_id = GetNullableInt(reader, "order_id") ?? 0,
                                order_number = GetNullableInt(reader, "order_id") ?? 0,
                                consumer_id = GetNullableInt(reader, "consumer_id") ?? 0,
                                shipping_address_id = GetNullableInt(reader, "shipping_address_id") ?? 0,
                                billing_address_id = GetNullableInt(reader, "billing_address_id") ?? 0,
                                delivery_description = GetNullableString(reader, "DeliveryDescription"),
                                payment_method = GetNullableString(reader, "PaymentMethod"),
                                payment_status = GetNullableString(reader, "payment_status"),
                                created_at = reader["created_at"] != DBNull.Value
                                    ? TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(reader["created_at"]),
                                        TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"))
                                        .ToString("yyyy-MM-dd HH:mm:ss")
                                    : null,
                                total = 0,
                                products = new List<OrderProductFKDto>(),
                                billing_address = new OrderAddressFKDto(),
                                shipping_address = new OrderAddressFKDto(),
                                sub_orders = new subordersFkResponseModel(),
                                order_status = new orderedStatus(),
                            };

                            orders.Add(order);
                        }

                        await reader.NextResultAsync();

                        // 2. Read Products
                        var products = new Dictionary<int, List<OrderProductFKDto>>();
                        var orderTotals = new Dictionary<int, decimal>();

                        while (await reader.ReadAsync())
                        {
                            var orderId = GetNullableInt(reader, "OrderID") ?? 0;

                            var product = new OrderProductFKDto
                            {
                                id = GetNullableInt(reader, "id") ?? 0,
                                name = GetNullableString(reader, "name"),
                                description = GetNullableString(reader, "description"),
                                price = GetNullableDecimal(reader, "price") ?? 0,
                                product_thumbnail = new AttachmentFK
                                {
                                    original_url = GetNullableString(reader, "product_thumbnail")
                                },
                                sale_price = GetNullableDecimal(reader, "sale_price") ?? 0,
                                discount = GetNullableInt(reader, "discount") ?? 0,
                                stock_status = GetNullableString(reader, "stock_status"),
                                quantity = GetNullableInt(reader, "Quantity") ?? 0,
                                unit = GetNullableString(reader, "unit"),
                                slug = GetNullableString(reader, "slug"),
                                type = GetNullableString(reader, "type"),
                                weight = GetNullableInt(reader, "weight") ?? 0,
                                status = true
                            };

                            if (!products.ContainsKey(orderId))
                                products[orderId] = new List<OrderProductFKDto>();

                            products[orderId].Add(product);

                            var subTotal = GetNullableDecimal(reader, "sub_total") ?? 0;
                            if (!orderTotals.ContainsKey(orderId))
                                orderTotals[orderId] = 0;

                            orderTotals[orderId] += subTotal;
                        }

                        await reader.NextResultAsync();

                        // 3. Read Addresses
                        var addresses = new Dictionary<int, OrderAddressFKDto>();

                        while (await reader.ReadAsync())
                        {
                            var addressId = GetNullableInt(reader, "id") ?? 0;

                            var address = new OrderAddressFKDto
                            {
                                id = addressId,
                                user_id = GetNullableInt(reader, "user_id") ?? 0,
                                Title = GetNullableString(reader, "Title"),
                                Street = GetNullableString(reader, "Street"),
                                State = GetNullableString(reader, "State"),
                                City = GetNullableString(reader, "City"),
                                Pincode = GetNullableInt(reader, "Pincode") ?? 0,
                                Phone = GetNullableString(reader, "Phone")
                            };

                            addresses[addressId] = address;
                        }

                        await reader.NextResultAsync();

                        // 4. Read Order Statuses
                        var orderStatuses = new Dictionary<int, orderedStatus>();

                        while (await reader.ReadAsync())
                        {
                            var orderId = GetNullableInt(reader, "OrderId") ?? 0;

                            var orderStatus = new orderedStatus
                            {
                                id = GetNullableInt(reader, "status_id") ?? 0,
                                orderId = orderId,
                                name = GetNullableString(reader, "status_name"),
                                slug = GetNullableString(reader, "slug"),
                                sequence = GetNullableString(reader, "sequence"),
                                status = reader["status"] != DBNull.Value && reader.GetBoolean(reader.GetOrdinal("status"))
                            };

                            orderStatuses[orderId] = orderStatus;
                        }

                        await reader.NextResultAsync();

                        // 5. Read Suborders
                        var suborders = new Dictionary<int, subordersFkResponseModel>();

                        while (await reader.ReadAsync())
                        {
                            var orderId = GetNullableInt(reader, "OrderID") ?? 0;

                            var suborder = new subordersFk
                            {
                                // Add fields if needed
                            };

                            if (!suborders.ContainsKey(orderId))
                                suborders[orderId] = new subordersFkResponseModel { subordersFk = new List<subordersFk>() };

                            suborders[orderId].subordersFk.Add(suborder);
                        }

                        // 6. Combine Data
                        foreach (var order in orders)
                        {
                            if (products.ContainsKey(order.order_id))
                                order.products = products[order.order_id];

                            if (addresses.ContainsKey(order.shipping_address_id))
                                order.shipping_address = addresses[order.shipping_address_id];

                            if (addresses.ContainsKey(order.billing_address_id))
                                order.billing_address = addresses[order.billing_address_id];

                            if (suborders.ContainsKey(order.order_id))
                                order.sub_orders = suborders[order.order_id];

                            if (orderStatuses.ContainsKey(order.order_id))
                                order.order_status = orderStatuses[order.order_id];

                            if (orderTotals.ContainsKey(order.order_id))
                                order.total = orderTotals[order.order_id];
                        }
                    }
                }
            }

            orderResponse.data = orders;
            orderResponse.total = orders.Count;
            return orderResponse;
        }

        public async Task<OrderFKResponseModel> GetAllOrders()
        {
            var orderResponse = new OrderFKResponseModel();
            var orders = new List<OrderFK>();

            using (var connection = (SqlConnection)_dbContext.Database.GetDbConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("GetAllOrders", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var order = new OrderFK
                            {
                                order_id = reader.GetInt32(reader.GetOrdinal("order_id")),
                                order_number = reader.GetInt32(reader.GetOrdinal("order_id")),
                                consumer_id = reader.GetInt32(reader.GetOrdinal("consumer_id")),
                                shipping_address_id = reader.GetInt32(reader.GetOrdinal("shipping_address_id")),
                                billing_address_id = reader.GetInt32(reader.GetOrdinal("billing_address_id")),
                                delivery_description = reader["DeliveryDescription"] as string,
                                payment_method = reader["PaymentMethod"] as string,
                                payment_status = reader["payment_status"] as string,
                                created_at = reader["created_at"] != DBNull.Value
                                    ? TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(reader["created_at"]),
                                        TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"))
                                        .ToString("yyyy-MM-dd HH:mm:ss")
                                    : null,
                                total = 0,
                                products = new List<OrderProductFKDto>(),
                                billing_address = new OrderAddressFKDto(),
                                shipping_address = new OrderAddressFKDto(),
                                sub_orders = new subordersFkResponseModel(),
                                order_status = new orderedStatus(),
                            };

                            orders.Add(order);
                        }

                        await reader.NextResultAsync();

                        var products = new Dictionary<int, List<OrderProductFKDto>>();
                        var orderTotals = new Dictionary<int, decimal>();

                        while (await reader.ReadAsync())
                        {
                            var orderId = reader.GetInt32(reader.GetOrdinal("OrderID"));

                            var product = new OrderProductFKDto
                            {
                                id = reader.GetInt32(reader.GetOrdinal("id")),
                                name = reader["name"] as string,
                                description = reader["description"] as string,
                                price = reader.GetDecimal(reader.GetOrdinal("price")),
                                product_thumbnail = new AttachmentFK
                                {
                                    original_url = reader["product_thumbnail"] as string
                                },
                                sale_price = reader.GetDecimal(reader.GetOrdinal("sale_price")),
                                discount = reader.GetInt32(reader.GetOrdinal("discount")),
                                stock_status = reader["stock_status"] as string,
                                quantity = reader.GetInt32(reader.GetOrdinal("Quantity")),
                                unit = reader["unit"] as string,
                                slug = reader["slug"] as string,
                                type = reader["type"] as string,
                                weight = reader.GetInt32(reader.GetOrdinal("weight")),
                                status = true
                            };

                            if (!products.ContainsKey(orderId))
                            {
                                products[orderId] = new List<OrderProductFKDto>();
                            }

                            products[orderId].Add(product);

                            var subTotal = reader.GetDecimal(reader.GetOrdinal("sub_total"));
                            if (!orderTotals.ContainsKey(orderId))
                            {
                                orderTotals[orderId] = 0;
                            }
                            orderTotals[orderId] += subTotal;
                        }

                        await reader.NextResultAsync();

                        var addresses = new Dictionary<int, OrderAddressFKDto>();

                        while (await reader.ReadAsync())
                        {
                            var id = reader.GetInt32(reader.GetOrdinal("id"));

                            var address = new OrderAddressFKDto
                            {
                                id = id,
                                user_id = reader.GetInt32(reader.GetOrdinal("user_id")),
                                Title = reader["Title"] as string,
                                Street = reader["Street"] as string,
                                State = reader["State"] as string,
                                City = reader["City"] as string,
                                Pincode = reader.GetInt32(reader.GetOrdinal("Pincode")),
                                Phone = reader["Phone"] as string
                            };

                            addresses[id] = address;
                        }

                        await reader.NextResultAsync();

                        var orderStatuses = new Dictionary<int, orderedStatus>();

                        while (await reader.ReadAsync())
                        {
                            var id = reader.GetInt32(reader.GetOrdinal("OrderId"));

                            var orderStatus = new orderedStatus
                            {
                                id = id,
                                orderId = reader.GetInt32(reader.GetOrdinal("OrderId")),
                                name = reader["name"] as string,
                                slug = reader["slug"] as string,
                                status = true
                            };

                            orderStatuses[id] = orderStatus;
                        }

                        await reader.NextResultAsync();

                        var suborders = new Dictionary<int, subordersFkResponseModel>();

                        while (await reader.ReadAsync())
                        {
                            var orderId = reader.GetInt32(reader.GetOrdinal("OrderID"));

                            var suborder = new subordersFk
                            {
                                // Fill with suborder fields
                            };

                            if (!suborders.ContainsKey(orderId))
                            {
                                suborders[orderId] = new subordersFkResponseModel { subordersFk = new List<subordersFk>() };
                            }

                            suborders[orderId].subordersFk.Add(suborder);
                        }

                        foreach (var order in orders)
                        {
                            if (products.ContainsKey(order.order_id))
                                order.products = products[order.order_id];

                            if (addresses.ContainsKey(order.shipping_address_id))
                                order.shipping_address = addresses[order.shipping_address_id];

                            if (addresses.ContainsKey(order.billing_address_id))
                                order.billing_address = addresses[order.billing_address_id];

                            if (suborders.ContainsKey(order.order_id))
                                order.sub_orders = suborders[order.order_id];

                            if (orderStatuses.ContainsKey(order.order_id))
                                order.order_status = orderStatuses[order.order_id];

                            if (orderTotals.ContainsKey(order.order_id))
                                order.total = orderTotals[order.order_id];
                        }
                    }
                }
            }

            orderResponse.data = orders;
            orderResponse.total = orders.Count;
            return orderResponse;
        }


        public async Task<OrderCancelledFKResponseModel> GetCancelledOrders()
        {
            var response = new OrderCancelledFKResponseModel();
            var orders = new List<OrdercancelledFK>();

            using var connection = (SqlConnection)_dbContext.Database.GetDbConnection();
            await connection.OpenAsync();

            using var command = new SqlCommand("GetCancelledOrders", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            using var reader = await command.ExecuteReaderAsync();

            // =========================
            // 1. Orders
            // =========================
            while (await reader.ReadAsync())
            {
                orders.Add(new OrdercancelledFK
                {
                    order_id = reader.GetInt32(reader.GetOrdinal("order_id")),
                    order_number = reader.GetInt32(reader.GetOrdinal("order_id")),
                    consumer_id = reader.GetInt32(reader.GetOrdinal("consumer_id")),
                    shipping_address_id = reader.GetInt32(reader.GetOrdinal("shipping_address_id")),
                    billing_address_id = reader.GetInt32(reader.GetOrdinal("billing_address_id")),
                    delivery_description = reader["DeliveryDescription"] as string,
                    payment_method = reader["PaymentMethod"] as string,
                    payment_status = reader["payment_status"] as string,
                    created_at = reader["created_at"] != DBNull.Value
                        ? TimeZoneInfo.ConvertTimeFromUtc(
                            Convert.ToDateTime(reader["created_at"]),
                            TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"))
                            .ToString("yyyy-MM-dd HH:mm:ss")
                        : null,
                    total = 0,
                    products = new List<OrderProductFKDto>(),
                    billing_address = null,
                    shipping_address = null,
                    order_status = null,
                    refund = null
                });
            }

            // =========================
            // 2. Products
            // =========================
            await reader.NextResultAsync();

            var products = new Dictionary<int, List<OrderProductFKDto>>();
            var totals = new Dictionary<int, decimal>();

            while (await reader.ReadAsync())
            {
                int orderId = reader.GetInt32(reader.GetOrdinal("OrderID"));

                var product = new OrderProductFKDto
                {
                    id = reader.GetInt32(reader.GetOrdinal("id")),
                    name = reader["name"] as string,
                    description = reader["description"] as string,
                    price = reader.GetDecimal(reader.GetOrdinal("price")),
                    sale_price = reader.GetDecimal(reader.GetOrdinal("sale_price")),
                    quantity = reader.GetInt32(reader.GetOrdinal("Quantity")),
                    discount = reader.GetInt32(reader.GetOrdinal("discount")),
                    stock_status = reader["stock_status"] as string,
                    unit = reader["unit"] as string,
                    slug = reader["slug"] as string,
                    type = reader["type"] as string,
                    weight = reader.GetInt32(reader.GetOrdinal("weight")),
                    status = true,
                    product_thumbnail = new AttachmentFK
                    {
                        original_url = reader["product_thumbnail"] as string
                    }
                };

                if (!products.ContainsKey(orderId))
                    products[orderId] = new List<OrderProductFKDto>();

                products[orderId].Add(product);

                decimal subTotal = reader.GetDecimal(reader.GetOrdinal("sub_total"));
                totals[orderId] = totals.ContainsKey(orderId)
                    ? totals[orderId] + subTotal
                    : subTotal;
            }

            // =========================
            // 3. Addresses
            // =========================
            await reader.NextResultAsync();

            var addresses = new Dictionary<int, OrderAddressFKDto>();

            while (await reader.ReadAsync())
            {
                int id = reader.GetInt32(reader.GetOrdinal("id"));

                addresses[id] = new OrderAddressFKDto
                {
                    id = id,
                    user_id = reader.GetInt32(reader.GetOrdinal("user_id")),
                    Title = reader["Title"] as string,
                    Street = reader["Street"] as string,
                    State = reader["State"] as string,
                    City = reader["City"] as string,
                    Pincode = reader.GetInt32(reader.GetOrdinal("Pincode")),
                    Phone = reader["Phone"] as string
                };
            }

            // =========================
            // 4. Order Status
            // =========================
            await reader.NextResultAsync();

            var statuses = new Dictionary<int, orderedStatus>();

            while (await reader.ReadAsync())
            {
                int orderId = reader.GetInt32(reader.GetOrdinal("OrderId"));

                statuses[orderId] = new orderedStatus
                {
                    id = reader.GetInt32(reader.GetOrdinal("Id")),
                    orderId = orderId,
                    name = reader["Name"] as string,
                    slug = reader["Slug"] as string,
                    status = true
                };
            }

            // =========================
            // 5. Refunds ✅
            // =========================
            await reader.NextResultAsync();

            var refunds = new Dictionary<int, OrderRefundDto>();

            while (await reader.ReadAsync())
            {
                int orderId = reader.GetInt32(reader.GetOrdinal("OrderId"));

                refunds[orderId] = new OrderRefundDto
                {
                    OrderId = orderId,
                    RefundId = reader.GetInt32(reader.GetOrdinal("RefundId")),
                    Status = reader["Status"] as string
                };
            }

            // =========================
            // Final Mapping
            // =========================
            foreach (var order in orders)
            {
                if (products.TryGetValue(order.order_id, out var prod))
                    order.products = prod;

                if (addresses.TryGetValue(order.shipping_address_id, out var ship))
                    order.shipping_address = ship;

                if (addresses.TryGetValue(order.billing_address_id, out var bill))
                    order.billing_address = bill;

                if (statuses.TryGetValue(order.order_id, out var status))
                    order.order_status = status;

                if (refunds.TryGetValue(order.order_id, out var refund))
                    order.refund = refund;

                if (totals.TryGetValue(order.order_id, out var total))
                    order.total = total;
            }

            response.data = orders;
            response.total = orders.Count;
            return response;
        }

        public async Task AddOrderDeliveryDetailAsync(OrderDeliveryDetail detail)
        {
            detail.CreatedAt = DateTime.Now;
            detail.UpdatedAt = DateTime.Now;

            _dbContext.OrderDeliveryDetails.Add(detail);
            await _dbContext.SaveChangesAsync();
        }

        public async Task EditOrderDeliveryDetailAsync(OrderDeliveryDetail detail)
        {
            var existingDetail = await _dbContext.OrderDeliveryDetails
                .FirstOrDefaultAsync(od => od.OrderId == detail.OrderId);  // Use OrderId to identify the record

            if (existingDetail == null)
            {
                throw new KeyNotFoundException($"No delivery detail found for OrderId {detail.OrderId}");
            }

            // Update properties if the delivery details already exist
            existingDetail.FullName = detail.FullName;
            existingDetail.DispatchDate = detail.DispatchDate;
            existingDetail.PostOfficeBranch = detail.PostOfficeBranch;
            existingDetail.TrackingNumber = detail.TrackingNumber;
            existingDetail.ExpectedDeliveryDate = detail.ExpectedDeliveryDate;
            existingDetail.SenderContact = detail.SenderContact;
            existingDetail.UpdatedAt = DateTime.Now; // Update the 'UpdatedAt' field

            await _dbContext.SaveChangesAsync();
        }


        public async Task<OrderDetailsDTO> GetOrderDetailsByOrderIdAsync(int orderId)
        {
            var result = new OrderDetailsDTO
            {
                OrderedProducts = new List<OrderedProduct>()
            };

            using var connection = _dbContext.Database.GetDbConnection();
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "GetOrderDetailsByOrderId";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@OrderID", orderId));

            using var reader = await command.ExecuteReaderAsync();

            // Ordered products
            while (await reader.ReadAsync())
            {
                result.OrderedProducts.Add(new OrderedProduct
                {
                    OrderedProductId = Convert.ToInt32(reader["OrderedProductId"]),
                    OrderID = Convert.ToInt32(reader["OrderID"]),
                    ProductId = Convert.ToInt32(reader["ProductId"]),
                    ProductName = reader["ProductName"].ToString(),
                    Description = reader["Description"].ToString(),
                    Quantity = Convert.ToInt32(reader["Quantity"]),
                    Price = Convert.ToDecimal(reader["Price"]),
                    ImageUrl = reader["ImageUrl"].ToString()
                });
            }

            // Shipping address
            if (await reader.NextResultAsync() && await reader.ReadAsync())
            {
                result.ShippingAddress = new ShippingAddress
                {
                    ShippingAddressId = Convert.ToInt32(reader["ShippingAddressId"]),
                    OrderID = Convert.ToInt32(reader["OrderID"]),
                    FullName = reader["FullName"].ToString(),
                    MobileNumber = reader["MobileNumber"].ToString(),
                    AlternateMobileNumber = reader["AlternateMobileNumber"].ToString(),
                    PinCode = (int)reader["PinCode"],
                    HouseNo = reader["HouseNo"].ToString(),
                    AreaDetails = reader["AreaDetails"].ToString(),
                    Landmark = reader["Landmark"].ToString(),
                    City = reader["City"].ToString(),
                    State = reader["State"].ToString(),
                    TypeOfAddress = reader["TypeOfAddress"].ToString(),
                    IsActive = Convert.ToBoolean(reader["IsActive"])
                };
            }

            // Order delivery details
            if (await reader.NextResultAsync() && await reader.ReadAsync())
            {
                result.DeliveryDetails = new OrderDeliveryDetails
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    OrderId = Convert.ToInt32(reader["OrderId"]),
                    FullName = reader["FullName"].ToString(),
                    DispatchDate = Convert.ToDateTime(reader["DispatchDate"]),
                    PostOfficeBranch = reader["PostOfficeBranch"].ToString(),
                    TrackingNumber = reader["TrackingNumber"].ToString(),
                    ExpectedDeliveryDate = Convert.ToDateTime(reader["ExpectedDeliveryDate"]),
                    SenderContact = reader["SenderContact"].ToString(),
                    CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                    UpdatedAt = Convert.ToDateTime(reader["UpdatedAt"])
                };
            }

            return result;
        }

        public async Task<OrderRefundDetailsDTO> GetOrderRefundDetailsByOrderIdAsync(int orderId)
        {
            var result = new OrderRefundDetailsDTO
            {
                OrderedProducts = new List<OrderedProduct>()
            };

            using var connection = _dbContext.Database.GetDbConnection();
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "GetOrderRefundDetailsByOrderId";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@OrderID", orderId));

            using var reader = await command.ExecuteReaderAsync();

            // 1️⃣ Ordered products
            while (await reader.ReadAsync())
            {
                result.OrderedProducts.Add(new OrderedProduct
                {
                    OrderedProductId = Convert.ToInt32(reader["OrderedProductId"]),
                    OrderID = Convert.ToInt32(reader["OrderID"]),
                    ProductId = Convert.ToInt32(reader["ProductId"]),
                    ProductName = reader["ProductName"].ToString(),
                    Description = reader["Description"].ToString(),
                    Quantity = Convert.ToInt32(reader["Quantity"]),
                    Price = Convert.ToDecimal(reader["Price"]),
                    ImageUrl = reader["ImageUrl"].ToString()
                });
            }

            // 2️⃣ Shipping address
            if (await reader.NextResultAsync() && await reader.ReadAsync())
            {
                result.ShippingAddress = new ShippingAddress
                {
                    ShippingAddressId = Convert.ToInt32(reader["ShippingAddressId"]),
                    OrderID = Convert.ToInt32(reader["OrderID"]),
                    FullName = reader["FullName"].ToString(),
                    MobileNumber = reader["MobileNumber"].ToString(),
                    AlternateMobileNumber = reader["AlternateMobileNumber"].ToString(),
                    PinCode = Convert.ToInt32(reader["PinCode"]),
                    HouseNo = reader["HouseNo"].ToString(),
                    AreaDetails = reader["AreaDetails"].ToString(),
                    Landmark = reader["Landmark"].ToString(),
                    City = reader["City"].ToString(),
                    State = reader["State"].ToString(),
                    TypeOfAddress = reader["TypeOfAddress"].ToString(),
                    IsActive = Convert.ToBoolean(reader["IsActive"])
                };
            }

            // 3️⃣ Refund details
            if (await reader.NextResultAsync() && await reader.ReadAsync())
            {
                var paymentType = reader["RefundPaymentType"]?.ToString();

                result.OrderRefundDetails = new CancelOrderRequest
                {
                    OrderId = Convert.ToInt32(reader["OrderId"]),
                    RefundPaymentType = paymentType,
                    RefundAmount = reader["RefundAmount"] != DBNull.Value
                        ? Convert.ToDecimal(reader["RefundAmount"])
                        : 0,

                    RefundUpiId = reader["UPIId"] != DBNull.Value
                        ? reader["UPIId"].ToString()
                        : null,

                    RefundBankDetails = paymentType == "bank"
                        ? new BankDetails
                        {
                            AccountName = reader["BankAccountName"]?.ToString(),
                            AccountNumber = reader["BankAccountNumber"]?.ToString(),
                            IfscCode = reader["BankIFSC"]?.ToString()
                        }
                        : null
                };
            }

            return result;
        }

        public async Task<bool> UpdateAsync(UpdateOrderStatus orderStatus)
        {
            var existing = await _dbContext.OrderStatus
                .FirstOrDefaultAsync(x => x.OrderId == orderStatus.OrderId);

            if (existing == null) return false;

            existing.name = orderStatus.name;
            existing.Slug = orderStatus.Slug;
            existing.status = orderStatus.status;
            existing.updated_At = DateTime.Now;

            switch (orderStatus.name?.Trim().ToLower())
            {
                case "processing":
                    existing.sequence = "2";
                    break;
                case "cancelled":
                    existing.sequence = "3";
                    break;
                case "shipped":
                    existing.sequence = "4";
                    break;
                case "out for delivery":
                    existing.sequence = "5";
                    break;
                case "delivered":
                    existing.sequence = "6";
                    break;
                default:
                    existing.sequence = "1"; // default or unknown
                    break;
            }

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<OrderDeliveryDetail> GetDeliveryDetailsByOrderIdAsync(int orderId)
        {
            return await _dbContext.OrderDeliveryDetails
                .FirstOrDefaultAsync(d => d.OrderId == orderId);
        }
        public async Task CancelOrderAsync(CancelOrderRequest request)
        {
            // Execute the updated SP with refund info
            await _dbContext.Database.ExecuteSqlRawAsync(
     @"EXEC sp_CancelOrder 
     @OrderId = {0}, 
     @Name = {1}, 
     @Slug = {2}, 
     @Sequence = {3}, 
     @Status = {4}, 
     @ReasonForCancel = {5}, 
     @RefundPaymentType = {6}, 
     @BankAccountName = {7}, 
     @BankAccountNumber = {8}, 
     @BankIFSC = {9}, 
     @UPIId = {10},
     @RefundAmount = {11}",
     request.OrderId,
     request.Name,
     request.Slug,
     request.Sequence,
     true, // default status as true
     request.ReasonForCancel,
     request.RefundPaymentType,
     request.RefundPaymentType == "bank" ? request.RefundBankDetails?.AccountName : null,
     request.RefundPaymentType == "bank" ? request.RefundBankDetails?.AccountNumber : null,
     request.RefundPaymentType == "bank" ? request.RefundBankDetails?.IfscCode : null,
     request.RefundPaymentType == "upi" ? request.RefundUpiId : null,
     request.RefundAmount
 );

            // Step 2: Get order cancellation info
            var orderCancelInfo = _dbContext.Set<CancelOrderInfo>()
                .FromSqlRaw("EXEC sp_GetOrderCancelInfo @OrderId = {0}", request.OrderId)
                .AsEnumerable()
                .FirstOrDefault();

            if (orderCancelInfo == null)
            {
                Console.WriteLine($"Order cancellation info not found for OrderId: {request.OrderId}");
                return;
            }

            // Step 3: Send cancellation email to user
            string subject = "Order Cancelled - Angel Book House";
            string body = $"Dear {orderCancelInfo.FirstName},\n\n" +
                          $"Your order (Order ID: {request.OrderId}) has been cancelled successfully for the following reason:\n" +
                          $"{request.ReasonForCancel}\n\n";

            if (!string.IsNullOrEmpty(request.RefundPaymentType))
            {
                body += "Refund Payment Method: " + request.RefundPaymentType.ToUpper() + "\n";

                if (request.RefundPaymentType == "bank" && request.RefundBankDetails != null)
                {
                    body += $"Bank Account: {request.RefundBankDetails.AccountName}, {request.RefundBankDetails.AccountNumber}, IFSC: {request.RefundBankDetails.IfscCode}\n";
                }
                else if (request.RefundPaymentType == "upi" && !string.IsNullOrEmpty(request.RefundUpiId))
                {
                    body += $"UPI ID: {request.RefundUpiId}\n";
                }
            }

            body += "\nThank you for shopping with us.\n\nAngel Book House";

            bool emailSentToUser = await _emailRepository.SendEmailAsync(
                orderCancelInfo.Email, subject, body, "Angel Book House", "amruth2118reddy@gmail.com"
            );

            // Step 4: Send notification email to admin
            string adminBody = $"Order Cancellation Alert:\n\n" +
                               $"User Name: {orderCancelInfo.FirstName} {orderCancelInfo.LastName}\n" +
                               $"Email: {orderCancelInfo.Email}\n" +
                               $"Order ID: {request.OrderId} was cancelled.\n\n" +
                               $"Reason: {request.ReasonForCancel}\n";

            if (!string.IsNullOrEmpty(request.RefundPaymentType))
            {
                adminBody += $"Refund Method: {request.RefundPaymentType}\n";
                if (request.RefundPaymentType == "bank" && request.RefundBankDetails != null)
                {
                    adminBody += $"Bank Account: {request.RefundBankDetails.AccountName}, {request.RefundBankDetails.AccountNumber}, IFSC: {request.RefundBankDetails.IfscCode}\n";
                }
                else if (request.RefundPaymentType == "upi" && !string.IsNullOrEmpty(request.RefundUpiId))
                {
                    adminBody += $"UPI ID: {request.RefundUpiId}\n";
                }
            }

            adminBody += "\nPlease check the Admin Panel for more details.";

            bool emailSentToAdmin = await _emailRepository.SendEmailAsync(
                "amruth2118reddy@gmail.com", subject, adminBody, "Angel Book House", "amruth2118reddy@gmail.com"
            );

            if (!emailSentToUser || !emailSentToAdmin)
            {
                Console.WriteLine("Order cancelled, but one or more emails failed to send.");
            }
        }


        //public async Task<dynamic> GetOrderByRazorpayOrderIdAsync1(string razorpayOrderId)
        //{
        //    var result = await _dbContext.Database
        //        .ExecuteSqlRawAsync("EXEC [dbo].[GetOrderByRazorpayOrderId] @RazorpayOrderId", new SqlParameter("@RazorpayOrderId", razorpayOrderId));

        //    return result;  // Return the result as dynamic (or map it to a DTO)
        //}

        public async Task<OrderDetails> GetOrderByRazorpayOrderIdAsync(string razorpayOrderId)
        {
            return await _dbContext.OrderDetails
                .FirstOrDefaultAsync(o => o.RazorpayOrderId == razorpayOrderId);
        }


        // Update order status
        public async Task<OrderDetails> UpdateOrderStatusAsync(string razorpayOrderId, string failureReason)
        {
            // Call the stored procedure to update the order status to "Failed"
            var result = await _dbContext.Database.ExecuteSqlRawAsync(
                "EXEC [dbo].[UpdateOrderToFailed] @RazorpayOrderId, @FailureReason",
                new SqlParameter("@RazorpayOrderId", razorpayOrderId),
                new SqlParameter("@FailureReason", failureReason)
            );

            // Fetch the updated order
            var updatedOrder = await GetOrderByRazorpayOrderIdAsync(razorpayOrderId);

            return updatedOrder;
        }

        public async Task<ShippingAddress> getshippingaddressbyorderId(int orderId)
        {
            return await _dbContext.ShippingAddress
                .FirstOrDefaultAsync(s => s.OrderID == orderId);
        }

        public async Task<bool> UpdateRefundStatusAsync(int orderId, string status)
        {
            var refund = await _dbContext.OrderRefunds
                                       .FirstOrDefaultAsync(r => r.OrderId == orderId);

            if (refund == null)
                return false;

            refund.Status = status;
            refund.Updated_At = DateTime.Now;

            await _dbContext.SaveChangesAsync();
            return true;
        }

    }


}