using EkartAPI.Models;
using EkartAPI.Helpers; 
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using EkartAPI.Repositories;

namespace EkartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesRepository _categoriesRepository;

        public CategoriesController(ICategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }

        [HttpGet("GetSubcategories/{categoryId}")]
        public async Task<IActionResult> GetSubcategories(int categoryId)
        {
            var subcategories = await _categoriesRepository.GetSubcategoriesByCategoryIdAsync(categoryId);

            if (subcategories == null)
            {
                return NotFound(new { message = "No subcategories found for the given category." });
            }

            return Ok(subcategories);
        }


        [HttpGet("GetAllCategories_FK")]
        public async Task<IActionResult> GetAllCategories_FK()
        {
            // Get only parent categories with nested subcategories
            var categories = await _categoriesRepository.GetAllUniqueCategoriesWithSubCategoriesAsync_FK();

            // Map to DTO
            //var response = CategoryMapper.MapCategoriesToDTO(categories);

            return Ok(categories);
        }

        [HttpGet("GetSectionIds_FK")]
        public async Task<IActionResult> GetSectionIds_FK()
        {
            var sections = await _categoriesRepository.GetSectionIdsAsync_FK();

            // Map to DTO
            //var response = CategoryMapper.MapCategoriesToDTO(categories);

            return Ok(sections);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            // Get only parent categories with nested subcategories
            var categories = await _categoriesRepository.GetAllUniqueCategoriesWithSubCategoriesAsync();

            // Map to DTO
            var response = CategoryMapper.MapCategoriesToDTO(categories);

            return Ok(response);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var category = await _categoriesRepository.GetCategoryByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            var categoryDto = CategoryMapper.MapCategoryToDTO(category);

            return Ok(categoryDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryCreateModel request)
        {
            var category = new Categories
            {
                CategoryName = request.CategoryName,
                ParentCategoryId = request.ParentCategoryId == 0 || request.ParentCategoryId == null ? (int?)null : request.ParentCategoryId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _categoriesRepository.AddCategoryAsync(category);

            var response = CategoryMapper.MapCategoryToDTO(category);

            return Ok(response);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, CategoryUpdateModel request)
        {
            // Check if the category exists
            var category = await _categoriesRepository.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound(new { message = "Category not found." });
            }

            // Update only the required fields
            await _categoriesRepository.UpdateCategoryAsync(id, request.CategoryName);

            return Ok(new { message = "Category updated successfully.", status = "success" });
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            // Check if the category exists
            var category = await _categoriesRepository.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound(new { message = "Category not found." });
            }

            // Delete the category along with its subcategories
            await _categoriesRepository.DeleteCategoryAsync(id);

            return Ok(new { message = "Category deleted successfully.", status = "success" });
        }
    }
}