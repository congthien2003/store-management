using StoreManagement.Application.DTOs.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Application.DTOs.Response
{
    public class CategoryResponse
    {
        public string Name { get; set; }
        public StoreDTO StoreDTO { get; set; }
    }
}
