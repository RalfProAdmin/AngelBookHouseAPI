using EkartAPI.Models;

namespace EkartAPI.Repository.Interface
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<CategoryModel>> GetAllCategoryAsync();
        Task AddCategoryAsync(CategoryModel category);
        Task UpdateCategoryAsync(CategoryModel category);
        Task<CategoryModel> GetUserByIdAsync(int categoryId);
        Task<List<Product>> GetUserByCategoryNameAsync(int categoryId);
        Task<List<CategoryModel>> GetDataForPageNProduct(int pageno, int pageSize);
    }
}
