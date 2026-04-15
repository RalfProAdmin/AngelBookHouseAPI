using System.ComponentModel.DataAnnotations;

namespace EkartAPI.Models
{
    public class OrderRefund
    {
        [Key]
        public int RefundId { get; set; }

        public int OrderId { get; set; }

        public string? RefundPaymentType { get; set; }

        public string? BankAccountName { get; set; }

        public string? BankAccountNumber { get; set; }

        public string? BankIFSC { get; set; }

        public string? UPIId { get; set; }

        public decimal RefundAmount { get; set; }

        public string Status { get; set; } = string.Empty;

        public DateTime Created_At { get; set; }

        public DateTime? Updated_At { get; set; }
    }

    public class UpdateRefundStatusRequest
    {
        [Required]
        public int OrderId { get; set; }

        [Required]
        public string Status { get; set; } = string.Empty;
    }
}
