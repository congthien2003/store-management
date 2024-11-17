using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Application.DTOs.Request.KPI
{
    public class UpdateKPIRequest
    {
        public double totalRevenue { get; set; }
        public double totalSales { get; set; }
        public int month {  get; set; }
        public int year { get; set; }
    }
}
