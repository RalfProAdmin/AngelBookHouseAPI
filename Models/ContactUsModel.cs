using System.ComponentModel.DataAnnotations;

namespace EkartAPI.Models
{
    public class ContactUsModel
    {
        [Key]
        public int ConatctusID {  get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt  { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class ContactDetails
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
