namespace EkartAPI.Models
{
    public class OtpSendRequest
    {
        public string MobileNumber { get; set; }
    }

    public class OtpVerifyRequest
    {
        public string SessionId { get; set; }
        public string Otp { get; set; }
    }

}
