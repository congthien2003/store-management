using StoreManagement.Application.DTOs.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Application.DTOs.Response
{
    public class FoodBestSellerResponse
    {
        public List<FoodDTO> ListFood { get; set; }
        public List<FoodDTO> Top4BestSeller { get; set; }
    }
}
