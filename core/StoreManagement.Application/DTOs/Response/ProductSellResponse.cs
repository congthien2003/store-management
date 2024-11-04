using StoreManagement.Application.DTOs.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Application.DTOs.Response
{
    public class ProductSellResponse
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public FoodDTO Food { get; set; }
    }
}
