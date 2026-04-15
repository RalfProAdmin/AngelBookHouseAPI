using EkartAPI.Models;
using EkartAPI.Models.ResponseModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EkartAPI.Repository.Interface
{
    public interface IProductRepository
    {
        //FK
        Task<ProductFKResponseModel> GetAllProductsAsync_FK();
        //End FK
        Task<IEnumerable<ProductByCategoryModel>> GetProductsByCategory();
        Task<IEnumerable<ProductResponceModel>> GetRecentProductsFromSPAsync();
        Task<Product> GetProductById(int id);
        Task<IEnumerable<Product>> GetProductsByCategory(int categoryId);
        Task<IEnumerable<Product>> GetAvailableProducts();
        Task<IEnumerable<Product>> SearchProducts(string keyword);
        Task<Product> AddProduct(Product product);
        Task<Product> UpdateProduct(Product product);
        Task<bool> DeleteProduct(int id);
        Task<List<Product>> Searchproducts(string productName);
        Task<List<Product>> GetDataForPageNProduct(int pageno, int pageSize);
        Task<int> GetProductsCountAsync();
        Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId);
        Task<IEnumerable<ProductStatusDto>> GetProductStatusesAsync();
        Task UpdateProductStatusAsync(int ProductId, bool TopSelling, bool TrendingProduct, bool RecentlyAdded);


    }
}
