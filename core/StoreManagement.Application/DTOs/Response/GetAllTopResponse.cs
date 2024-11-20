using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Application.DTOs.Response
{
    public class GetAllTopResponse
    {
        public FoodResponse Top1 { get; set; }
        public FoodResponse Top2 { get; set; }
        public FoodResponse Top3 { get; set; }
        public List<FoodResponse> Others { get; set; }
    }
}
