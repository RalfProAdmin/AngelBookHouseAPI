namespace EkartAPI.Repository.Interface
{
    public interface ISmsRepository
    {
        Task<string> SendMessageAsync(string mobileNumber, string message);
        Task SendOrderPlacedwhatsappAsync(string customerPhone, string message);
    }
}
