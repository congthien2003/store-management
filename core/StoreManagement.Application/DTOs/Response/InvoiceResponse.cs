using StoreManagement.Application.DTOs.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Application.DTOs.Response
{
    public class InvoiceResponse
    {
        public int Id { get; set; }
        public DateTime FinishedAt { get; set; }
        public bool Status { get; set; }
        public int TotalOrder { get; set; }
        public double Charge { get; set; }
        public OrderDTO OrderDTO { get; set; }
        public PaymentTypeDTO PaymentTypeDTO { get; set; }
        public VoucherDTO VoucherDTO { get; set; }
    }
}
