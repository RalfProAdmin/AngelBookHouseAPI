using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EkartAPI.Models.ResponseModels
{
    public class CanceledOrder
    {
        [Key]
        public int CancelId { get; set; }
        public int OrderId { get; set; }
        public string OrderStatus { get; set; }
        public string ReasonForCancel { get; set; }
        public bool IsActive { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime? Updated_At { get; set; }
    }

public class CancelOrderRequest
    {
        public int OrderId { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Sequence { get; set; }

        public string ReasonForCancel { get; set; }
        public decimal RefundAmount { get; set; }

        // Refund info
        public string RefundPaymentType { get; set; } // "bank" or "upi"

        [NotMapped]
        public BankDetails? RefundBankDetails { get; set; }

        public string? RefundUpiId { get; set; }

    }

    public class BankDetails
    {
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public string IfscCode { get; set; }
    }

}