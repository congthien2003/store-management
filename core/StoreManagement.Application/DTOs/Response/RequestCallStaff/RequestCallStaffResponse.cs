namespace StoreManagement.Application.DTOs.Response.RequestCallStaff
{
    public class RequestCallStaffResponse
    {
        public int id { get; set; }
        public DateTime request_time { get; set; }
        public string table_number { get; set; }
        public int handled_by { get; set; }
        public string status { get; set; }
    }
}
