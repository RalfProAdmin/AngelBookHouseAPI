using System.Collections.Generic;
using System.Threading.Tasks;
using EkartAPI.Models;
using EkartAPI.Models.ResponseModels;

namespace EkartAPI.Repository.Interface
{
    public interface IUserRepository
    {

        //fk

        Task RegisterUserAsync(RegisterFKResponseModel user);

        //Endfk
        Task<userModel> GetUserByIdAsync(int userId);
        Task<IEnumerable<userModel>> GetAllUsersAsync();
        Task AddUserAsync(userModel user);
        Task UpdateUserAsync(userModel user);
        Task DeleteUserAsync(int userId);
        Task UpdateUserNameAsync(int userId, string firstName, string lastName);
        Task UpdateUserEmailAsync(int userId, string email);
        Task UpdateUserPhoneNumberAsync(int userId, string mobileNumber);
        Task<List<userModel>> GetRecentUsersFromSPAsync();
        Task<List<userModel>> GetDataForPageNProduct(int pageno, int pageSize);
        Task<int> GetUsersCountAsync();
        Task UpdateUserDetailsAsync(int id,UpdateUserDetails userDetails);

        Task UpdatePasswordAsync(int id, string hashedPassword);



    }
}
 