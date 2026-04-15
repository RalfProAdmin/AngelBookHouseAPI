using EkartAPI.Models;

namespace EkartAPI.Repository.Interface
{
    public interface IContactRepository
    {
        Task<ContactInfoDto> GetContactInfoAsync();
        Task UpdateContactInfoAsync(ContactInfoDto contactInfoDto);
        Task<ContactSetting?> GetLatestEnquiryMessageAsync();
        Task<List<string>> GetEmailsAsync();
        Task<List<string>> GetMobileNumbersAsync();
    }
}
