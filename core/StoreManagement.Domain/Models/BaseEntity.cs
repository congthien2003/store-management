namespace StoreManagement.Domain.Models
{
    public class BaseEntity
    {
        public virtual int Id { get; set; }
        public Guid Guid { get; set; } = Guid.NewGuid();
    }
}
