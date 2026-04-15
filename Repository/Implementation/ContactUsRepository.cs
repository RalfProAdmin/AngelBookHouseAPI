using EkartAPI.Data;
using EkartAPI.Models;
using EkartAPI.Repository.Interface;

namespace EkartAPI.Repository.Implementation
{
    public class ContactUsRepository: IContactUsRepository
    {

        private readonly EkartDBcontext _dbContext;

        public ContactUsRepository(EkartDBcontext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ContactDetails> AddContactDetails(ContactDetails contactDetail)
        {
            var entity = new ContactUsModel
            {
                Name = contactDetail.Name,
                Email = contactDetail.Email,
                Phone = contactDetail.Phone,
                Subject = contactDetail.Subject,
                Message = contactDetail.Message,
                IsActive=true,
                CreatedAt=DateTime.UtcNow,
                UpdatedAt=DateTime.UtcNow
            };

            _dbContext.ContactUsDetails.Add(entity);
            await _dbContext.SaveChangesAsync();

            // return DTO back
            return new ContactDetails
            {
                Name = entity.Name,
                Email = entity.Email,
                Phone = entity.Phone,
                Subject = entity.Subject,
                Message = entity.Message
            };
        }


    }
}
