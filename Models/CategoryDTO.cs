namespace EkartAPI.Models
{
    public class CategoryDTO
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int? ParentCategoryId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<CategoryDTO> SubCategories { get; set; } = new List<CategoryDTO>();
    }
}
