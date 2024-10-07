using StoreManagement.Application.DTOs.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Application.DTOs.Response
{
    public class FoodResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public int Quantity { get; set; }
        public string ImageUrl { get; set; }
        public decimal? Price { get; set; }
        public CategoryDTO CategoryDTO { get; set; }
    }
}
