using StoreManagement.Domain.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreManagement.Domain.Models
{
    public class Ticket : DeleteableEntity
    {
        public int Status { get; set; } = (int)ETicketStatus.Pending;
        public string Title { get; set; }
        public string Description { get; set; }
        public int RequestBy { get; set; }
        [ForeignKey("RequestBy")]
        public User User { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }
}
