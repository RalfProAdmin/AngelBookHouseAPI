namespace EkartAPI.Models.ResponseModels
{
    public class ResetPasswordDTO
    {
        public string Email { get; set; }
        public string password { get; set; }
        public string password_confirmation { get; set; }
        public string otp { get; set; }
    }
}
