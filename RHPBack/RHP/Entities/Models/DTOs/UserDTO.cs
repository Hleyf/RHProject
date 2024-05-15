using RHP.Entities.Models;

namespace RHP.Entities.Models.DTOs
{
    public class UserDTO
    {
        public int id { get; set; }
        public required string email { get; set; }
        public UserRole role { get; set; }
    }
}