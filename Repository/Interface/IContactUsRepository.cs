using EkartAPI.Models;

namespace EkartAPI.Repository.Interface
{
    public interface IContactUsRepository
    {
        Task<ContactDetails> AddContactDetails(ContactDetails ContactDetails);
    }
}
