﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Application.DTOs.Request
{
    public class TicketDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Status { get; set; } 
        public DateTime Created { get; set; } = DateTime.Now;   
        public int RequestBy {  get; set; }
    }
}
