namespace EkartAPI.Models.ResponseModels
{
    public class RegisterFKResponseModel
    {
        public string name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string password { get; set; }
        public string password_confirmation { get; set; }
    }
}
