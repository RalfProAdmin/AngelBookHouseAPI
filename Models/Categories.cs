using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EkartAPI.Models
{
    public class Categories
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(100)]
        public string CategoryName { get; set; }

        // ParentCategoryId is nullable to allow top-level categories
        public int? ParentCategoryId { get; set; }

        // Self-referencing relationship for subcategories
        [ForeignKey("ParentCategoryId")]
        public virtual Categories ParentCategory { get; set; }

        public virtual ICollection<Categories> SubCategories { get; set; } = new List<Categories>();

        // Timestamps
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}




