﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreManagement.Application.DTOs.Request.Store;

namespace StoreManagement.Application.DTOs.Response
{
    public class TableResponse
    {
        public int Id { get; set; }
        public string Guid { get; set; }
        public bool Status { get; set; }
		public int IdStore {  get; set; }
    }
}
