﻿using StoreManagement.Domain.Enum;

namespace StoreManagement.Application.DTOs.Request
{
    public class OrderDetailDTO
    {
        public int Quantity { get; set; }
        public int IdOrder { get; set; }
        public int IdFood { get; set; }
	
	
    }
}
