using EkartAPI.Data;
using EkartAPI.Models;
using EkartAPI.Models.ResponseModels;
using EkartAPI.Repository.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace EkartAPI.Repository.Implementation
{
    public class WishListRepository : IWishListRepository
    {
        private readonly EkartDBcontext _dbContext;

        public WishListRepository(EkartDBcontext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> AddToWishListAsync(int userId, int productId)
        {
            // Define the output parameter for the message
            var messageParam = new SqlParameter("@Message", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };

            // Execute the stored procedure to add item to the wishlist
            await _dbContext.Database.ExecuteSqlRawAsync(
                "EXEC AddToWishList @UserId, @ProductId, @Message OUTPUT",
                new SqlParameter("@UserId", userId),
                new SqlParameter("@ProductId", productId),
                messageParam
            );

            // Return the message
            return messageParam.Value.ToString();
        }

        public async Task<ProductFKResponseModel> GetAllWishlistItemsFKAsync(int userId)
        {
            var products = await _dbContext.Set<Product>()
                .FromSqlRaw("EXEC GetUserWishlistProducts @UserId = {0}", userId)
                .ToListAsync();

            List<ProductFK> lstProductFK = new List<ProductFK>();

            foreach (var product in products)
            {
                ProductFK productFKResponseModel = new()
                {
                    id = product.ProductId,
                    name = product.ProductName,
                    description = product.Description ?? "",
                    price = Convert.ToInt32(product.Price),
                    product_thumbnail = new() { original_url = product.ImageUrl },
                    sale_price = Convert.ToInt32(product.Price),
                    discount = 0,
                    stock_status = Convert.ToInt32(product.Availability) > 0 ? "in_stock" : "sold_out",
                    unit = "",
                    slug = product.ProductName.ToLower().Replace(" ", "-"),
                    type = "product",
                    weight = 0,
                    status = true,
                    categories = GetParentCategoryIds(product.CategoryId)
                    //categories = ((product.ProductId == 77 || product.ProductId == 78)? new List<Category>() { new Category() { slug = "28" } } : new List<Category>() { new Category() { slug = "22" } })
                };

                lstProductFK.Add(productFKResponseModel);
            }

            return new ProductFKResponseModel() { data = lstProductFK, total = lstProductFK.Count };
        }

        private List<Category> GetParentCategoryIds(int CategoryId)
        {
            return _dbContext.Set<Category>()
                .FromSqlRaw("EXEC GetParentCategoriesByProductId {0}", CategoryId)
                .ToList();
        }

        public async Task<IEnumerable<WishItemDTO>> GetAllWishlistItemsAsync(int userId)
        {
            return await _dbContext.WishList
                .Where(w => w.UserId == userId)
                .Join(_dbContext.tbl_products,
                    wish => wish.ProductId,
                    product => product.ProductId,
                    (wish, product) => new WishItemDTO
                    {
                        WishId = wish.WishId,
                        UserId = wish.UserId,
                        ProductId = wish.ProductId,
                        ProductName = product.ProductName,
                        ImageUrl = product.ImageUrl,
                        Price = product.Price
                    })
                .ToListAsync();
        }

        public async Task<WishList> GetWishlistItemByIdAsync(int wishId)
        {
            return await _dbContext.WishList.FindAsync(wishId);
        }

        public async Task<WishList> GetWishlistDetailsByUserAndProductId(int userId, int productId)
        {
            return await _dbContext.WishList
                                   .FirstOrDefaultAsync(w => w.UserId == userId && w.ProductId == productId);
        }

        public async Task<bool> DeleteWishItem(int productId)
        {
            // Execute the stored procedure to delete by ProductId
            var result = await _dbContext.Database.ExecuteSqlRawAsync(
                "EXEC [dbo].[DeleteWishListByProductId] @ProductId",
                new SqlParameter("@ProductId", productId)
            );

            // Check if the delete operation affected any rows
            if (result > 0)
            {
                return true; // Successfully deleted the item
            }

            return false; // No record was deleted
        }


        public async Task<string> GetProductExistOrNotInWishList(int userId, int productId)
        {
            try
            {
                using (var command = _dbContext.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "CheckWishlistExistence";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@UserId", userId));
                    command.Parameters.Add(new SqlParameter("@ProductId", productId));

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
                return "Error occurred while checking the wishlist";
            }

            return "Item not found or an error occurred";
        }

    }
}
