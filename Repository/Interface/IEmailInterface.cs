namespace EkartAPI.Repository.Interface
{
    public interface IEmailInterface
    {
        Task<bool> SendEmailAsync(string toEmail, string subject, string body, string senderName, string senderEmail);
    }
}
