﻿using StoreManagement.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StoreManagement.Domain.Models
{
    public class User : DeleteableEntity
    {
        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string Username { get; set; } = string.Empty;
        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string Email { get; set; }
        public string? Phones { get; set; } 
        public string Password { get; set; }
        public int Role { get; set; } = 1;
        public Store Store { get; set; }
        public Staff Staff { get; set; }
    }
}
