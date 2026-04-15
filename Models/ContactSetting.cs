namespace EkartAPI.Models
{
    public class ContactSetting
    {
        public int Id { get; set; }
        public string EnquiryMessage { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
    }

}
