using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Domain.Models
{
    public class KPI : BaseEntity
    {
        public int Month { get; set; } = DateTime.Now.Month;
        public int Year { get; set; } = DateTime.Now.Year;
        public double TotalSales { get; set; } = 0;
        public double TotalRevenue { get; set; } = 0;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
