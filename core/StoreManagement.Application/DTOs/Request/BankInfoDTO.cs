using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Application.DTOs.Request
{
    public class BankInfoDTO
    {
        public int IdStore { get; set; }
        public string BankId { get; set; }
        public string BankName { get; set; }
        public string AccountName { get; set; }
        public string AccountId { get; set; }

    }
}
