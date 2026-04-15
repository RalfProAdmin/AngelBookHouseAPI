namespace EkartAPI.Models
{
    public class ContactInfoDto
    {
        public string EnquiryMessage { get; set; }
        public List<string> Emails { get; set; }
        public List<string> MobileNumbers { get; set; }
    }

}
