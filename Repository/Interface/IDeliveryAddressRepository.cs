using EkartAPI.Models;
using EkartAPI.Models.ResponseModels;

namespace EkartAPI.Repository.Interface
{
    public interface IDeliveryAddressRepository
    {
        Task<DeliveryAddressModel> AddDeliveryAddress(DeliveryAddressModel address);
        //Task<DeliveryAddressModel> AddDeliveryAddress(DeliveryAddressModel product);
        Task<IEnumerable<DeliveryAddressModel>> GetDeliveryAddressByUserId(int UserId);
        Task<DeliveryAddressModel> GetDeliveryAddressById(int id);
        Task<DeliveryAddressModel> UpdateDeliveryAddress(DeliveryAddressModel deliveryAddress);
        Task<bool> DeleteDeliveryAddress(int id);
    }
}
