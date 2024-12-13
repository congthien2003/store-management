using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreManagement.Domain.Models
{
    public class Order : DeleteableEntity
    {
        public decimal Total { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public int IdTable { get; set; }
        [ForeignKey("IdTable")]
        public Table Table { get; set; }

        public int IdInvoice { get; set; }
        [ForeignKey("IdInvoice")]

        public bool hasInvoice { get; set; } = false;
        public virtual Collection<OrderDetail> OrderDetails { get; set; }
    }
}
