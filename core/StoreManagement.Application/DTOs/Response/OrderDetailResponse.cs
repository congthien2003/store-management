using StoreManagement.Application.DTOs.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Application.DTOs.Response
{
    public class OrderDetailResponse
    {
        public int Quantity { get; set; }
        public OrderDTO OrderDTO { get; set; }
        public FoodDTO FoodDTO { get; set; }
    }
}
