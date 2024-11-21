using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Domain.Models
{
    public class BankInfo : DeleteableEntity
    {
        public string BankId { get; set; }
        public string BankName { get; set; }

        public string AccountName { get; set; }
        public string AccountId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int IdStore { get; set; }
        [ForeignKey("IdStore")]
        public Store Store { get; set; }
    }
}
