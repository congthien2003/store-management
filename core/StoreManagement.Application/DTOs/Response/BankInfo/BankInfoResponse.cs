using StoreManagement.Application.DTOs.Request.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Application.DTOs.Response.BankInfo
{
    public class BankInfoResponse
    {
        public string BankId { get; set; }
        public string BankName { get; set; }

        public string AccountName { get; set; }
        public string AccountId { get; set; }

        public int IdStore { get; set; }
        public int Id {  get; set; }
    }
}
