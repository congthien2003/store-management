using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Application.DTOs.Response
{
    public class UserResponse
    {
        public Guid Guid { get; set; }
        public int Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Phones { get; set; }
        public int Role { get; set; }
    }
}
