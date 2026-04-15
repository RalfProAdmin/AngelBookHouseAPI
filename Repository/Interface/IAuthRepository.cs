using EkartAPI.Models;
using EkartAPI.Models.ResponseModels;
using Microsoft.AspNetCore.Mvc;

namespace EkartAPI.Repository.Interface
{
    public interface IAuthRepository
    {
        //string AuthenticateUser(string email, string password);

        //FK
        Task<IActionResult> LoginFK(string email, string password);
        //End FK

        Task<IActionResult> AuthenticateUser(string email, string password);

        UserInfoDTO GetUserInformation(string email);
        Task<IActionResult> RequestPasswordReset(string email);
        Task<IActionResult> VerifyOtp(string email, string otp);
        Task<IActionResult> ResetPassword(string email, string password, string password_confirmation);

    }
}
