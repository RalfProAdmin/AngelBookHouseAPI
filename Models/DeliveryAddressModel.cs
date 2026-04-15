using System.ComponentModel.DataAnnotations;

namespace EkartAPI.Models
{
    public class DeliveryAddressModel
    {
        [Key]
        public int AddId {  get; set; }

        public int UserId { get; set; }
        public string FullName { get; set; }

        public string MobileNumber { get; set; }

        public string AlternateMobileNumber { get; set; }

      
        public int PinCode { get; set; }

        public string HouseNo { get; set; }

    
        public string AreaDetails { get; set; }

        public string Landmark { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string TypeOfAddress { get; set; }


        public bool IsActive { get; set; }

        public DateTime createAt { get; set; }

        public DateTime updateAt { get; set; }
    }
}
