using EkartAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace EkartAPI.Helpers
{
    public static class CategoryMapper
    {
            public static CategoryDTO MapCategoryToDTO(Categories category)
            {
                return new CategoryDTO
                {
                    CategoryId = category.CategoryId,
                    CategoryName = category.CategoryName,
                    ParentCategoryId = category.ParentCategoryId,
                    CreatedAt = category.CreatedAt,
                    UpdatedAt = category.UpdatedAt,
                    SubCategories = category.SubCategories?
                        .Select(subCategory => MapCategoryToDTO(subCategory))
                        .ToList() ?? new List<CategoryDTO>() // Recursive call to map subcategories
                };
            }


        public static List<CategoryDTO> MapCategoriesToDTO(List<Categories> categories)
        {
            return categories.Select(category => MapCategoryToDTO(category)).ToList();
        }
    }
}