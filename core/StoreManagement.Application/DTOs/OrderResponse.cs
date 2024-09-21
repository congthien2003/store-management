using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Application.DTOs
{
    public class OrderResponse
    {
        public double Total { get; set; }
        public string NameUser { get; set; }
        public string PhoneUser { get; set; }
        public int IdTable { get; set; }
    }
}
