using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using EkartAPI.Data;
using EkartAPI.Models;
using EkartAPI.Models.ResponseModels;
using EkartAPI.Repository.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace EkartAPI.Repository.Implementation
{
    public class CartRepository : ICartRepository
    {
        private readonly EkartDBcontext _dbContext;

        public CartRepository(EkartDBcontext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddToCartAsync(Cart cart)
        {
            cart.CreatedDate = DateTime.Now;
            await _dbContext.tbl_cart.AddAsync(cart);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<CartItemDTO>> GetAllCartItemsAsync(int userId)
        {
            return await _dbContext.tbl_cart
                .Where(c => c.UserId == userId)
                .Join(_dbContext.tbl_products,
                    cart => cart.ProductId,
                    product => product.ProductId,
                    (cart, product) => new CartItemDTO
                    {
                        CartId = cart.CartId,
                        UserId = cart.UserId,
                        ProductId = cart.ProductId,
                        Quantity = cart.Quantity,
                        CreatedDate = cart.CreatedDate,
                        ProductName = product.ProductName,
                        ImageUrl = product.ImageUrl,
                        Price = product.Price
                    })
                .ToListAsync();
        }

        public async Task<Cart> GetCartItemByIdAsync(int cartId)
        {
            return await _dbContext.tbl_cart.FindAsync(cartId);
        }

        public async Task UpdateCartItemAsync(Cart cart)
        {
            cart.CreatedDate = DateTime.Now;
            _dbContext.tbl_cart.Update(cart);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteCartItemAsync(int cartId)
        {
            var cart = await _dbContext.tbl_cart.FindAsync(cartId);
            if (cart != null)
            {
                _dbContext.tbl_cart.Remove(cart);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<Cart> GetCartDetailsByUserAndProductId(int userId, int productId)
        {
            return await _dbContext.tbl_cart
                                   .FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == productId);
        }

        public async Task<Cart> DeleteCartItemByProductId(int ProductId)
        {
            var existingOrg = await _dbContext.tbl_cart.FirstOrDefaultAsync(x => x.ProductId == ProductId);
            if (existingOrg is null)
            {
                return null;
            }

            _dbContext.tbl_cart.Remove(existingOrg);
            await _dbContext.SaveChangesAsync();
            return existingOrg;
        }

        public async Task<CartFKResponseModel> GetCartItemByIdFKAsync(int userId)
        {
            var response = new CartFKResponseModel
            {
                items = new List<CartFK>()
            };

            // Opening connection asynchronously
            await using (var connection = _dbContext.Database.GetDbConnection())
            {
                var command = connection.CreateCommand();
                command.CommandText = "GetCartDetailsByUserId"; // Stored procedure name
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@UserId", userId));

                await connection.OpenAsync(); // Open connection asynchronously
                using (var reader = await command.ExecuteReaderAsync()) // Execute asynchronously
                {
                    while (await reader.ReadAsync()) // Read data asynchronously
                    {
                        var item = new CartFK
                        {
                            id = reader.GetInt32(reader.GetOrdinal("CartId")),
                            product_id = reader.GetInt32(reader.GetOrdinal("ProductId")),
                            quantity = reader.GetInt32(reader.GetOrdinal("Quantity")),
                            sub_total = reader.GetDecimal(reader.GetOrdinal("SubTotal")),  // Use GetDecimal for decimal values
                            price = reader.GetDecimal(reader.GetOrdinal("Price")),         // Use GetDecimal for decimal values
                            variation = new Variation
                            {
                                price = reader.GetDecimal(reader.GetOrdinal("Price")),     // Use GetDecimal for decimal values
                                sale_price = reader.GetDecimal(reader.GetOrdinal("Sale_Price")),  // Use GetDecimal for decimal values
                                name = reader.GetString(reader.GetOrdinal("ProductName")),
                            },
                            product = new ProductsFK
                            {
                                id = reader.GetInt32(reader.GetOrdinal("ProductId")),
                                name = reader.GetString(reader.GetOrdinal("ProductName")),
                                description = reader.GetString(reader.GetOrdinal("Description")),
                                price = reader.GetDecimal(reader.GetOrdinal("Price")),     // Use GetDecimal for decimal values
                                sale_price = reader.GetDecimal(reader.GetOrdinal("Sale_Price")),  // Use GetDecimal for decimal values
                                product_thumbnail = new Attachment
                                {
                                    original_url = reader.GetString(reader.GetOrdinal("ProductThumbnail"))
                                }
                            }
                        };


                        response.items.Add(item);
                    }
                }
            }

            // Calculate total and open state (modify as needed)
            response.total = response.items.Sum(x => x.sub_total);
            response.stickyCartOpen = true;  // Placeholder
            response.sidebarCartOpen = true; // Placeholder

            return response;
        }

        public async Task UpdateCartQuantity(int userId, int productId, int quantityChange)
        {
            await _dbContext.Database.ExecuteSqlRawAsync(
                "EXEC UpdateCartQuantity @p0, @p1, @p2",
                userId, productId, quantityChange
            );
        }

        public async Task InsertCartItemsAsync(List<CartItem> cartItems)
        {
            var table = new DataTable();
            table.Columns.Add("UserId", typeof(int));
            table.Columns.Add("ProductId", typeof(int));
            table.Columns.Add("Quantity", typeof(int));
            table.Columns.Add("CreatedDate", typeof(DateTime));
            foreach (var item in cartItems)
            {
                table.Rows.Add(item.UserId, item.ProductId, item.Quantity, item.CreatedDate);
            }

            var connection = (SqlConnection)_dbContext.Database.GetDbConnection();

            await using var command = new SqlCommand("sp_InsertCartItems", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            var param = command.Parameters.AddWithValue("@CartItems", table);
            param.SqlDbType = SqlDbType.Structured;
            param.TypeName = "CartItemType";

            if (connection.State != ConnectionState.Open)
                await connection.OpenAsync();

            await command.ExecuteNonQueryAsync();
        }
    }
}
