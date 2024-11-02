using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreManagement.Application.DTOs.Request;

namespace StoreManagement.Application.DTOs.Response
{
    public class StoreResponse
    {
        public Guid Guid { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public UserDTO? UserDTO { get; set; }

    }
}
