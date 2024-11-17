using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Application.DTOs.Response.KPI
{
    public class KPIResponse
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public int TotalSales { get; set; }
        public int TotalRevenue { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
