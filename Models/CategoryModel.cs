using System.ComponentModel.DataAnnotations;

namespace EkartAPI.Models
{
    public class CategoryModel
    {
        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Brief { get; set; }
        public string Icons { get; set; }
        public int Priority { get; set; }

        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
