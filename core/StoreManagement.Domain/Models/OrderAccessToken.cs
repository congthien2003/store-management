using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Domain.Models
{
    public class OrderAccessToken
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string QRURL { get; set; }
        public int? IdOrder { get; set; }
        public bool IsActived { get; set; } = true;
        public bool IsPaid { get; set; } = false;
        public DateTime ExpirationTime { get; set; } = DateTime.Now.AddHours(1);
        public DateTime TimeCreated { get; set; } = DateTime.Now;

    }
}
