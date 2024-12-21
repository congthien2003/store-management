using System.ComponentModel.DataAnnotations.Schema;

namespace StoreManagement.Domain.Models
{
    public class ComboItem
    {
        public int Id { get; set; }
        public int IdCombo { get; set; }
        [ForeignKey("IdCombo")]
        public Combo Combo { get; set; }
        public int IdFood { get; set; }
        [ForeignKey("IdCombo")]
        public Food Food { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
