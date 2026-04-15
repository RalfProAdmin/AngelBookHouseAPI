using EkartAPI.Data;
using EkartAPI.Models;
using EkartAPI.Repository.Interface;
using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;

namespace EkartAPI.Repository.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {

        private readonly EkartDBcontext _dbContext;

        public CategoryRepository(EkartDBcontext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddCategoryAsync(CategoryModel category)
        {
            await _dbContext.TblCategory.AddAsync(category);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<CategoryModel>> GetAllCategoryAsync()
        {
            return await _dbContext.TblCategory.ToListAsync();
        }

        public async Task<List<Product>> GetUserByCategoryNameAsync(int categoryId)
        {
            return await _dbContext.tbl_products.Where(p => p.CategoryId == categoryId).ToListAsync();
        }

        public async Task<CategoryModel> GetUserByIdAsync(int categoryId)
        {
            return await _dbContext.TblCategory.FindAsync(categoryId);
        }

        public async Task UpdateCategoryAsync(CategoryModel category)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CategoryModel>> GetDataForPageNProduct(int pageno, int pageSize)
        {
            return await _dbContext.TblCategory
                .FromSqlRaw("exec getCategoriesByPageNoOrdersCount {0}, {1}", pageno, pageSize)
                .ToListAsync();
        }
    }
}
