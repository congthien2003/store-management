using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Application.DTOs.Auth
{
    public class CheckToken
    {
        public string Role { get; set; }
        public bool IsDenied { get; set; }
    }
}
