﻿namespace StoreManagement.Application.DTOs.Request
{
    public class VoucherDTO
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public int Discount { get; set; }
        public int IdStore { get; set; }
    }
}