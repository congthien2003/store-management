using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Application.DTOs.Response
{
    public class GetAllFoodResponse
    {
        public FoodResponse Top1 { get; set; }
        public FoodResponse Top2 { get; set; }
        public FoodResponse Top3 { get; set; }
        public List<FoodResponse> Others { get; set; }
    }
}
