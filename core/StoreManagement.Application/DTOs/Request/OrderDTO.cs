﻿namespace StoreManagement.Application.DTOs.Request
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public double Total { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool CreatedInvoice { get; set; }
        public int IdTable { get; set; }
        public bool Status { get; set; }


    }
}
