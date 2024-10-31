using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Application.DTOs.Request
{
    public class FoodTopDTO
    {
        public List<FoodDTO> AllFoods {  get; set; }
        public List<ProductSellDTO> Top3ProductsByQuantity { get; set; }
    }
}
