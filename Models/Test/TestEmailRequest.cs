namespace EkartAPI.Models.Test
{
    public class TestEmailRequest
    {
        public string ToEmail { get; set; } = string.Empty;
        public string Subject { get; set; } = "Test Email";
        public string Body { get; set; } = "This is a test email from Angel Book House.";
    }
}
