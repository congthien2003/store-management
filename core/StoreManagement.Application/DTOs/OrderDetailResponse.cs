using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Application.DTOs
{
    public class OrderDetailResponse
    {
        public int Quantity { get; set; }
        public int IdOrder { get; set; }
        public int IdFood { get; set; }
    }
}
