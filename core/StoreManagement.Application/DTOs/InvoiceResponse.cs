using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Application.DTOs
{
    public class InvoiceResponse
    {
        public DateTime FinishedAt { get; set; }
        public bool Status { get; set; }
        public int TotalOrder { get; set; }
        public double Charge { get; set; }
        public int IdOrder { get; set; }
        public int IdPaymentType { get; set; }
        public int? IdVoucher { get; set; }
    }
}
