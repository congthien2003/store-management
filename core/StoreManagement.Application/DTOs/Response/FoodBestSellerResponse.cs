using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreManagement.Application.DTOs.Request;

namespace StoreManagement.Application.DTOs.Response
{
    public class FoodBestSellerResponse
    {
        public List<FoodDTO> AllFoods { get; set; }
        public List<FoodDTO> Top4ProductsByQuantity { get; set; }
    }
}
