using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Application.DTOs
{
    public class VoucherResponse
    {
        public string Name { get; set; }
        public int Discount { get; set; }
        public int IdStore { get; set; }
    }
}
