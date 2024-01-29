using RHP.Entities.Interfaces;

namespace RHP.Entities.Models
{
    public enum UserRole
    {
        Player,
        Admin
    }
    public class User : IBaseEntity
    {
        public int Id => UserId;
        public int UserId { get; set; }
        public required string Email { get; set; }

        public required string Password { get; set; }

        public UserRole Role { get; set; } = UserRole.Player;

        public override string? ToString() => Email;
    }
}
