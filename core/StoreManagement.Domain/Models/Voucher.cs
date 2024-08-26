using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreManagement.Domain.Models
{
    public class Voucher : DeleteableEntity
    {
        public string Name { get; set; }    
        public int Discount { get; set; }
        public virtual Collection<Invoice> Invoices { get; set; }
        public int IdStore { get; set; }
        [ForeignKey("IdStore")]
        public Store Store { get; set; }
    }
}
