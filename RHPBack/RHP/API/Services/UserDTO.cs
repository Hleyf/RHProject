using RHP.Entities.Models;

namespace RHP.API.Services
{
    public class UserDTO
    {
        public int Id { get; set; }
        public required string Email { get; set; }
        public UserRole Role { get; set; }
    }
}