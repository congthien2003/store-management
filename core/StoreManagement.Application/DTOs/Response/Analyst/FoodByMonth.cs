﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Application.DTOs.Response.Analyst
{
    public class FoodByMonth
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public int Quantity { get; set; }
    }
}
