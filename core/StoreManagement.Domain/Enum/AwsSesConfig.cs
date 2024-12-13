using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Domain.Enum
{
    public class AwsSesConfig
    {
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
        public string SenderEmail { get; set; }
        public string Region { get; set; }
    }
}
