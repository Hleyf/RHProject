using RHP.Entities.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace RHP.Entities.Models
{
    public enum UserRole
    {
        Player,
        Admin
    }
    public class User : IBaseEntity
    {
        [Key]
        public int Id { get; set; }
        public required string Email { get; set; }

        public required string Password { get; set; }

        public Player? Player { get; set; }

        public UserRole Role { get; set; } = UserRole.Player;

        public bool active { get; set; } = true;

        public override string? ToString() => Email;
    }
}
