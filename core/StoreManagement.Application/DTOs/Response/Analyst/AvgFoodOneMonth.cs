using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Application.DTOs.Response.Analyst
{
    public class AvgFoodOneMonth
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public double Average { get; set; }
    }
}
