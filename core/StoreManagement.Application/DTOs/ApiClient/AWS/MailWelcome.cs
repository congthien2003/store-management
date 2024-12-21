using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Application.DTOs.ApiClient.AWS
{
    public class MailWelcome
    {
        public string RecipientEmail { get; set; }
        public string Name { get; set; }
    }
}
