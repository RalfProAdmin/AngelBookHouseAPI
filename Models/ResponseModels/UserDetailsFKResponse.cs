namespace EkartAPI.Models.ResponseModels
{
    public class UserDetailsFKResponse
    {
        public int user_id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
    }

    public class UpdateUserDetails
    {
        public int id { get; set; }
        public string name { get; set; }
        
        public string phone { get; set; }
    }

    public class UpdatePasswordFK
    {
        public string current_password { get; set; }
        public string password { get; set; }
        public string confirm_password { get; set; }

    }

}
