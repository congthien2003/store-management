using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Application.DTOs.Request
{
    public class StaffDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public int IdStore { get; set; }
        public DateTime CreatedAt { get; set; }
        public int IdUser { get; set; }
    }
}
