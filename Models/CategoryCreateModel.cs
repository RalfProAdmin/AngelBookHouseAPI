using System.ComponentModel.DataAnnotations;

namespace EkartAPI.Models
{
    public class CategoryCreateModel
    {
        [Required]
        [StringLength(100)]
        public string CategoryName { get; set; }

        public int? ParentCategoryId { get; set; } // Use 0 to indicate no parent category
    }
}
