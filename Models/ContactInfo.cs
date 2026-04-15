namespace EkartAPI.Models
{
    public class ContactInfo
    {
        public int Id { get; set; }
        public string ContactType { get; set; } // "email" or "mobile"
        public string Value { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}

