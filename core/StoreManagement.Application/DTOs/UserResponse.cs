using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Application.DTOs
{
    public class UserResponse
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Phones { get; set; }
        public int Role { get; set; }
    }
}
