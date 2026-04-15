namespace EkartAPI.Models
{
    public class UpdateNameRequest
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class UpdateEmailRequest
    {
        public int UserId { get; set; }
        public string Email { get; set; }
    }

    public class UpdatePhoneRequest
    {
        public int UserId { get; set; }
        public string MobileNumber { get; set; }
    }
}
