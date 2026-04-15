using EkartAPI.Data;
using EkartAPI.Models;
using EkartAPI.Models.ResponseModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EkartAPI.Repositories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly EkartDBcontext _context;

        public CategoriesRepository(EkartDBcontext context)
        {
            _context = context;
        }

        public async Task<CategoryFKResponseModel> GetAllUniqueCategoriesWithSubCategoriesAsync_FK()
        {
            var categories = await _context.Categories
                .Include(c => c.SubCategories)
                .ThenInclude(sc => sc.SubCategories)
                .ToListAsync();

            // Remove duplicates: only keep categories that are not already subcategories
            var uniqueCategories = categories.Where(c => c.ParentCategoryId == null).ToList();

            List<CategoryFK> lstCategoryFK = new List<CategoryFK>();
            foreach(var category in uniqueCategories)
            {
                CategoryFK categoryFKResponseModel = new()
                {
                    id=category.CategoryId,
                    name = category.CategoryName,
                    description = "",
                    slug = category.CategoryId.ToString(),
                    type = (category.ParentCategoryId==null?"product":""),
                    status = true,
                    parent_id = category.ParentCategoryId??0,
                    products_count = 1
                };
                lstCategoryFK.Add(categoryFKResponseModel);
            }

            return new CategoryFKResponseModel() { data = lstCategoryFK, total = lstCategoryFK.Count };
        }

        public async Task<SubCategoryFKResponseModel> GetSubcategoriesByCategoryIdAsync(int categoryId)
        {
            var lstSubCategoryFK = await _context.Set<SubCategoryFK>()
                .FromSqlRaw("EXEC GetSubcategoriesByCategoryId @CategoryId = {0}", categoryId)
                .ToListAsync();

            return new SubCategoryFKResponseModel() { data = lstSubCategoryFK, total = lstSubCategoryFK.Count };
        }

        public async Task<SectionFKResponseModel> GetSectionIdsAsync_FK()
        {
            var itopSellingIds = await _context.tbl_products.Where(p => p.topSelling == true).Select(p => p.ProductId).ToArrayAsync();
            var itrendingProductIds = await _context.tbl_products.Where(p => p.trendingProduct == true).Select(p => p.ProductId).ToArrayAsync();
            var irecentlyAddedIds = await _context.tbl_products.Where(p => p.recentlyAdded == true).Select(p => p.ProductId).ToArrayAsync();

            return new SectionFKResponseModel() { data = new SectionFK() { topSellingIds = itopSellingIds, trendingProductIds = itrendingProductIds, recentlyAddedIds = irecentlyAddedIds  }, total = 1 };
        }

        public async Task<List<Categories>> GetAllUniqueCategoriesWithSubCategoriesAsync()
        {
            var categories = await _context.Categories
                .Include(c => c.SubCategories)
                .ThenInclude(sc => sc.SubCategories)
                .ToListAsync();

            // Remove duplicates: only keep categories that are not already subcategories
            var uniqueCategories = categories.Where(c => c.ParentCategoryId == null).ToList();

            return uniqueCategories;
        }





        public async Task<Categories> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories
                .Include(c => c.SubCategories)
                    .ThenInclude(sc => sc.SubCategories) // Include subcategories of subcategories
                    .ThenInclude(sc2 => sc2.SubCategories) // Include further levels if needed
                .FirstOrDefaultAsync(c => c.CategoryId == id);
        }

        public async Task AddCategoryAsync(Categories category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync(int categoryId, string categoryName)
        {
            // Call the stored procedure to update the category
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC UpdateCategory @CategoryId = {0}, @CategoryName = {1}",
                categoryId,
                categoryName);
        }

        public async Task DeleteCategoryAsync(int categoryId)
        {
            // Execute the stored procedure to delete the category and its subcategories
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC DeleteCategoryWithSubcategories @CategoryId = {0}",
                categoryId);
        }
    }
}
