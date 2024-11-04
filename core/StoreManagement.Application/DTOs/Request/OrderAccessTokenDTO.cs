using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Application.DTOs.Request
{
        public class OrderAccessTokenDTO
        {
            public Guid Id { get; set; } = Guid.NewGuid();
            public string QRURL { get; set; }
            public int? IdOrder { get; set; }
            public bool IsActived { get; set; } = true;
            public bool IsPaid { get; set; } = false;
    }
}
