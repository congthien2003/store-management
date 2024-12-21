using System.ComponentModel.DataAnnotations.Schema;

namespace StoreManagement.Domain.Models
{
    public class Staff : DeleteableEntity
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public int Type { get; set; }
        public string Address { get; set; } = string.Empty;
        public int IdStore { get; set; }
        [ForeignKey("IdStore")]
        public Store Store { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int IdUser { get; set; }
        [ForeignKey("IdUser")]
        public User User { get; set; }


    }
}
