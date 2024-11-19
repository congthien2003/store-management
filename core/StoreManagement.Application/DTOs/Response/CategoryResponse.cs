using StoreManagement.Application.DTOs.Request.Store;

namespace StoreManagement.Application.DTOs.Response
{
    public class CategoryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public StoreDTO StoreDTO { get; set; }
    }
}
