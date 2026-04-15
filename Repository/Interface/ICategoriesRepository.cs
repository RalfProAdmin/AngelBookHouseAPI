using System.Collections.Generic;
using System.Threading.Tasks;
using EkartAPI.Models;
using EkartAPI.Models.ResponseModels;
using Microsoft.EntityFrameworkCore;

namespace EkartAPI.Repositories
{
    public interface ICategoriesRepository
    {
        //FK
        Task<CategoryFKResponseModel> GetAllUniqueCategoriesWithSubCategoriesAsync_FK();
        Task<SectionFKResponseModel> GetSectionIdsAsync_FK();
        Task<SubCategoryFKResponseModel> GetSubcategoriesByCategoryIdAsync(int categoryId);
        //End FK

        Task<List<Categories>> GetAllUniqueCategoriesWithSubCategoriesAsync();
        Task<Categories> GetCategoryByIdAsync(int id);
        Task AddCategoryAsync(Categories category);
        Task UpdateCategoryAsync(int categoryId, string categoryName);
        Task DeleteCategoryAsync(int id);
    }
}
