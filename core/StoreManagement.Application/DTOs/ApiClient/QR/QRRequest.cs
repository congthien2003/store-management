using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Application.DTOs.ApiClient.QR
{
    public class QRRequest
    {
        public string BankId { get; set; }
        public string AccountNo { get; set; }
        public string AccountName { get; set; }
        public double Amount { get; set; }

    }
}
