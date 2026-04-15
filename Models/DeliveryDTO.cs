using System.ComponentModel.DataAnnotations;

public class DeliveryDTO
{
    [Required]
    public int UserId { get; set; }

    [Required]
    public string FullName { get; set; }

    [Required]
    [Phone]
    public string MobileNumber { get; set; }

    [Phone]
    public string AlternateMobileNumber { get; set; }

    [Required]
    [Range(100000, 999999, ErrorMessage = "PinCode must be a 6-digit number")]
    public int PinCode { get; set; }

    [Required]
    public string HouseNo { get; set; }

    public string AreaDetails { get; set; }

    public string Landmark { get; set; }

    [Required]
    public string City { get; set; }

    [Required]
    public string State { get; set; }

    [Required]
    public string TypeOfAddress { get; set; }
}
