using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Application.DTOs.ApiClient.AWS
{
    public class EmailMonthlyReport
    {
        public string RecipientEmail {get; set;}
        public int IdStore {get; set;}
        public string StartDate {get; set;}
        public string EndDate {get; set;}
    }
}
