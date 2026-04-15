namespace EkartAPI.Models.ResponseModels
{
    public class forgotpasswordreponse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Email { get; set; }
        public string otp { get; set; }
        public DateTime expires_at { get; set; }


    }
}
