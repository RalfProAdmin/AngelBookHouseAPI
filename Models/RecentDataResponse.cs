using EkartAPI.Models.ResponseModels;

namespace EkartAPI.Models
{
    public class RecentDataResponse
    {

        public List<userModel> Users { get; set; }
        //public List<OrdersResModel> Orders { get; set; }
        public List<ProductResponceModel> Products { get; set; }
    }
}
