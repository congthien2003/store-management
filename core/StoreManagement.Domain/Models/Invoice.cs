using System.ComponentModel.DataAnnotations.Schema;
using StoreManagement.Domain.Models;

namespace StoreManagement.Domain.Models
{
    public class Invoice : DeleteableEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime FinishedAt { get; set; }
        public bool Status { get; set; }
        public int TotalOrder { get; set; }
        public double Charge { get; set; }
        public int IdPaymentType { get; set; }
        [ForeignKey("IdPaymentType")]
        public PaymentType PaymentType { get; set; }
        public int IdOrder { get; set; }
        [ForeignKey("IdOrder")]
        public Order Order { get; set; }
        public int? IdVoucher { get; set; }
        [ForeignKey("IdVoucher")]
        public Voucher Voucher { get; set; }
        
    }
}
