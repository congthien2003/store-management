using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Application.DTOs.Response.Analyst
{
    public class RevenueByMonth
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public double Total { get; set; }
    }
}
