using StoreManagement.Application.DTOs.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Application.DTOs.Response
{
    public class OrderResponse
    {
        public int Id { get; set; }
        public double Total { get; set; }
        public string NameUser { get; set; }
        public string PhoneUser { get; set; }
        public TableDTO TableDTO { get; set; }
    }
}
