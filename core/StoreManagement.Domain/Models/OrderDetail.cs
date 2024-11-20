using StoreManagement.Domain.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreManagement.Domain.Models
{
    public class OrderDetail
    {
        public int Quantity { get; set; }
        public int IdOrder { get; set; }
        [ForeignKey("IdOrder")]
        public Order Order { get; set; }
        public int IdFood { get; set; }
        [ForeignKey("IdFood")]
        public Food Food { get; set; }
        public int StatusProcess { get; set; } = (int) EStatusProcess.Pending;
        //public bool IsHoliday { get; set;} = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
