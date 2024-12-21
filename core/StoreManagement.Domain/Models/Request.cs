using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace StoreManagement.Domain.Models
{
    [Table("Requests")]
    public class Request : BaseModel
    {
        [PrimaryKey("id")]
        public int Id { get; set; }

        [Column("table_number")]
        public string TableNumber { get; set; }

        [Column("request_time")]
        public DateTime RequestTime { get; set; }

        [Column("status")]
        public string Status { get; set; }

        [Column("handled_by")]
        public int? HandledBy { get; set; }
    }
}
