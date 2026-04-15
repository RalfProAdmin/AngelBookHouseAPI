
using EkartAPI.Data;
using EkartAPI.Models;
using EkartAPI.Models.ResponseModels;
using EkartAPI.Repository.Interface;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EkartAPI.Repository.Implementation
{
    public class ProductRepository : IProductRepository
    {
        private readonly EkartDBcontext _dbContext;

        public ProductRepository(EkartDBcontext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<ProductFKResponseModel> GetAllProductsAsync_FK()
        {
            var products = await _dbContext.tbl_products.Where(p=> p.IsActive==true).ToListAsync();
            List<ProductFK> lstProductFK = new List<ProductFK>();

            foreach (var product in products)
            {
                // Convert decimal price to int safely
                int price = Convert.ToInt32(Math.Round(product.Price, MidpointRounding.AwayFromZero));

                // Extract numeric discount using Regex
                int discount = 0;
                if (!string.IsNullOrEmpty(product.Offer))
                {
                    Match match = Regex.Match(product.Offer, @"\d+"); // Extract only numbers
                    discount = match.Success ? int.Parse(match.Value) : 0;
                }

                // Calculate sale price: price - (price * discount / 100)
                int salePrice = price - ((price * discount) / 100);

                // Convert Availability safely
                bool isAvailable = int.TryParse(product.Availability, out int availability) && availability > 0;

                ProductFK productFKResponseModel = new()
                {
                    id = product.ProductId,
                    name = product.ProductName,
                    description = product.Description ?? "",
                    price = price,
                    product_thumbnail = new() { original_url = product.ImageUrl },
                    sale_price = salePrice, // Updated sale price with discount
                    discount = discount,    // Discount as an integer
                    stock =  product.Availability,
                    stock_status = isAvailable ? "in_stock" : "sold_out",
                    Enquiry = product.Enquiry,
                    IsUsed = product.IsUsed,
                    unit = "",
                    slug = product.ProductName.ToLower().Replace(" ", "-"),
                    type = "product",
                    weight = 0,
                    status = true,
                    categories = GetParentCategoryIds(product.CategoryId)
                };

                lstProductFK.Add(productFKResponseModel);
            }

            return new ProductFKResponseModel() { data = lstProductFK, total = lstProductFK.Count };
        }
        //

        private List<Category> GetParentCategoryIds(int CategoryId)
        {
            return _dbContext.Set<Category>()
                .FromSqlRaw("EXEC GetParentCategoriesByProductId {0}", CategoryId)
                .ToList();
        }


        public async Task<IEnumerable<ProductByCategoryModel>> GetProductsByCategory()
        {
            // Use ADO.NET-style query with FromSqlRaw
            return await _dbContext.Set<ProductByCategoryModel>()
                .FromSqlRaw("EXEC GetProductsByTopCategory")
                .ToListAsync();
        }


        public async Task<Product> GetProductById(int id)
        {
            return await _dbContext.tbl_products.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> GetProductsByCategory(int categoryId)
        {
            return await _dbContext.tbl_products.Where(p => p.CategoryId == categoryId).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAvailableProducts()
        {
            return await _dbContext.tbl_products.Where(p => p.Availability == "Available").ToListAsync();
        }

        public async Task<IEnumerable<Product>> SearchProducts(string keyword)
        {
            return await _dbContext.tbl_products.Where(p => p.ProductName.Contains(keyword) || p.Description.Contains(keyword)).ToListAsync();
        }

        public async Task<Product> AddProduct(Product product)
        {
            product.CreatedAt = DateTime.Now;
            _dbContext.tbl_products.Add(product);
            await _dbContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            product.UpdatedAt = DateTime.Now;
            _dbContext.Entry(product).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var product = await _dbContext.tbl_products.FindAsync(id);
            if (product == null)
            {
                return false;
            }

            // Soft Delete
            product.IsActive = false;
            product.UpdatedAt = DateTime.Now;

            _dbContext.tbl_products.Update(product);
            await _dbContext.SaveChangesAsync();

            return true;
        }


        public async Task<List<Product>> Searchproducts(string productName)
        {
            return await _dbContext.tbl_products
                .Where(p => EF.Functions.Like(p.ProductName, $"%{productName}%"))
                .ToListAsync();
        }

        public async Task<IEnumerable<ProductResponceModel>> GetRecentProductsFromSPAsync()
        {
            return await _dbContext.RecentProducts
                .FromSqlRaw("EXEC Sp_GetRecentProducts")
                .ToListAsync();
        }


        public async Task<List<Product>> GetDataForPageNProduct(int pageno, int pageSize)
        {
            return await _dbContext.tbl_products
                .FromSqlRaw("exec getproducts {0}, {1}", pageno, pageSize)
                .ToListAsync();
        }


        public async Task<int> GetProductsCountAsync()
        {
            var result = await _dbContext.Set<userCount>()
                                       .FromSqlRaw("EXEC GetProductsRowCount")
                                       .ToListAsync();

            return result.FirstOrDefault()?.TotalRowCount ?? 0;
        }
        public async Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId)
        {
            // Get all category IDs (including parent and subcategories) using a recursive query
            var categoryIds = await GetCategoryAndSubcategoriesAsync(categoryId);

            // Query products by those category IDs
            var products = await _dbContext.tbl_products
                .Where(p => categoryIds.Contains(p.CategoryId))
                .ToListAsync();

            return products;
        }

        private async Task<List<int>> GetCategoryAndSubcategoriesAsync(int categoryId)
        {
            // Fetch all categories and their subcategories recursively
            var categoryIds = new List<int>();

            // Recursive function to fetch all subcategories
            async Task FetchSubcategories(int currentCategoryId)
            {
                categoryIds.Add(currentCategoryId);
                var subcategories = await _dbContext.Categories
                    .Where(c => c.ParentCategoryId == currentCategoryId)
                    .Select(c => c.CategoryId)
                    .ToListAsync();

                foreach (var subCategoryId in subcategories)
                {
                    await FetchSubcategories(subCategoryId);
                }
            }

            await FetchSubcategories(categoryId);

            return categoryIds;
        }

        public async Task<IEnumerable<ProductStatusDto>> GetProductStatusesAsync()
        {

            return await _dbContext.Set<ProductStatusDto>()
                .FromSqlRaw("EXEC GetProductStatus")
                .ToListAsync();
        }


        public async Task UpdateProductStatusAsync(int ProductId, bool TopSelling, bool TrendingProduct, bool RecentlyAdded)
        {
            await _dbContext.Database.ExecuteSqlRawAsync(
              "EXEC UpdateProductStatus @ProductId = {0}, @TopSelling = {1},  @TrendingProduct = {2},@RecentlyAdded = {3}",
               ProductId, TopSelling, TrendingProduct, RecentlyAdded);
        }
    }
}
