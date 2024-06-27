﻿using RHP.Entities.Models;

namespace RHP.Entities.Models.DTOs
{
    public class UserDTO
    {
        public string Id { get; set; }
        public required string Email { get; set; }
        public UserRole Role { get; set; }
    }
}