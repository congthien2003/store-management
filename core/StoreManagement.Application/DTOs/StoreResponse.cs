using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Application.DTOs
{
    public class StoreResponse
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int IdUser { get; set; }
    }
}
