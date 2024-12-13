using System.ComponentModel.DataAnnotations.Schema;

namespace StoreManagement.Domain.Models
{
    public class Combo : DeleteableEntity
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public bool Status { get; set; } = false;
        public int IdStore { get; set; }
        [ForeignKey("IdStore")]
        public Store Store { get; set; }

        public virtual ICollection<ComboItem> ComboItems { get; set; }

    }
}
