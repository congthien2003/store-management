using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Application.DTOs.Response.Analyst
{
    public class MonthlySummaryReport
    {
        public int ShopID { get; set; }
        public int Year { get; set; }
        public int Month1 { get; set; }
        public int Month2 { get; set; }
        public int Month3 { get; set; }
        public double TotalRevenueMonth1 { get; set; }
        public double TotalRevenueMonth2 { get; set; }
        public double TotalRevenueMonth3 { get; set; }
        public int TotalFoodSoldMonth1 { get; set; }
        public int TotalFoodSoldMonth2 { get; set; }
        public int TotalFoodSoldMonth3 { get; set; }
        public int TotalOrderMonth1 { get; set; }
        public int TotalOrderMonth2 { get; set; }
        public int TotalOrderMonth3 { get; set; }
        public double AvgFoodPerOrderMonth1 { get; set; }
        public double AvgFoodPerOrderMonth2 { get; set; }
        public double AvgFoodPerOrderMonth3 { get; set; }
        public int DaysInMonth1 { get; set; }
        public int DaysInMonth2 { get; set; }
        public int DaysInMonth3 { get; set; }
        public int HolidaysCountMonth1 { get; set; }
        public int HolidaysCountMonth2 { get; set; }
        public int HolidaysCountMonth3 { get; set; }
        public string Quarter1 { get; set; }
        public string Quarter2 { get; set; }
        public string Quarter3 { get; set; }
        public double AvgOrderValue { get; set; }
    }
}
