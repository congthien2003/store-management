namespace StoreManagement.Application.DTOs.Request.OrderDetail
{
    public class UpdateStatusReq
    {
        public int IdOrder { get; set; }
        public int StatusProcess { get; set; }
        public int IdFood { get; set; }
    }
}
