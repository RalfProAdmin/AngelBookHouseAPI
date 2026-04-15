using EkartAPI.Migrations;
using Microsoft.AspNet.Identity;

namespace EkartAPI.Models
{
    public class LatestDataResponse
    {
        public IEnumerable<Product> LatestProducts { get; set; }
        public IEnumerable<OrdersModel> LatestOrders { get; set; }
        public IEnumerable<userModel> LatestUsers { get; set; }
    }
}
