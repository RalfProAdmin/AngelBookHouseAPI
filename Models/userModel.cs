using System;
using System.ComponentModel.DataAnnotations;

namespace EkartAPI.Models
{
    public class userModel
    {
        [Key]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public string Password { get; set; }
        public bool isActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class userCount
    {
        public int TotalRowCount { get; set; }
    }
}
