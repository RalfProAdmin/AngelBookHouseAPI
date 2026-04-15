namespace EkartAPI.Repository.Interface
{
    public interface IWhatsAppRepository
    {
        Task SendOrderConfirmationAsync(
            string mobileNumber,
            string customerName,
            string orderId,
            string orderDate
        );
    }
}
