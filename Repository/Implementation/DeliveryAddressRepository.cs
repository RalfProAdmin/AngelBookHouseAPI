using EkartAPI.Data;
using EkartAPI.Models;
using EkartAPI.Models.ResponseModels;
using EkartAPI.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace EkartAPI.Repository.Implementation
{
    public class DeliveryAddressRepository : IDeliveryAddressRepository
    {
        private readonly EkartDBcontext _dbContext;

        public DeliveryAddressRepository(EkartDBcontext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<DeliveryAddressModel> AddDeliveryAddress(DeliveryAddressModel address)
        {

            var entity = new DeliveryAddressModel
            {
                UserId =address.UserId,
                TypeOfAddress = address.TypeOfAddress,
                AreaDetails = address.AreaDetails,
                State = address.State,
                City = address.City,
                PinCode = address.PinCode,
                MobileNumber = address.MobileNumber,
                AlternateMobileNumber = address.MobileNumber,
                FullName = address.FullName,
                HouseNo = "",
                Landmark = "",
                IsActive = true,
                createAt = DateTime.UtcNow,
                updateAt = DateTime.UtcNow


            };

            _dbContext.tbl_DeliveryAddress.Add(entity);
            await _dbContext.SaveChangesAsync();

            return address;
        }
        //public async Task<DeliveryAddressModel> 
            
        //    AddDeliveryAddress(DeliveryAddressModel address)
        //{
        //    _dbContext.tbl_DeliveryAddress.Add(address);
        //    await _dbContext.SaveChangesAsync();
        //    return address;
        //}

        public async Task<bool> DeleteDeliveryAddress(int id)
        {
            var deliveryAddress = await _dbContext.tbl_DeliveryAddress.FindAsync(id);
            if (deliveryAddress == null)
            {
                return false;
            }

            _dbContext.tbl_DeliveryAddress.Remove(deliveryAddress);
            await _dbContext.SaveChangesAsync();
            return true;
        }


        public async Task<IEnumerable<DeliveryAddressModel>> GetDeliveryAddressByUserId(int UserId)
        {
            return await _dbContext.tbl_DeliveryAddress.Where(x => x.UserId == UserId && x.IsActive == true).ToListAsync();
        }

        public async Task<DeliveryAddressModel> UpdateDeliveryAddress(DeliveryAddressModel deliveryAddress)
        {
            _dbContext.Entry(deliveryAddress).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return deliveryAddress;
        }

        public async Task<DeliveryAddressModel> GetDeliveryAddressById(int id)
        {
                return await _dbContext.tbl_DeliveryAddress.FindAsync(id);
        }

    }
} 
