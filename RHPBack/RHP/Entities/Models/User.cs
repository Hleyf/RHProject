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
        public int id { get; set; }
        public required string email { get; set; }

        public required string password { get; set; }

        public Player? player { get; set; }

        public UserRole role { get; set; } = UserRole.Player;

        public List<User> contacts { get; set; } = []; 

        public bool active { get; set; } = true; //TODO: Must be set false once email autentication is implemented

        public override string? ToString() => email;
    }
}
