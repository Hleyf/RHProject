using RHP.Entities.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace RHP.Entities.Models
{
    public enum UserRole
    {
        Player,
        Admin
    }

    public enum UserStatus
    {
        Online,
        Away,
        Offline
    }
    public class User : IBaseEntity
    {
        [Key]
        public int Id { get; set; }
        public required string Email { get; set; }

        public required string Password { get; set; }

        public Player? Player { get; set; }

        public bool active { get; set; } 

        public UserRole Role { get; set; }

        public bool loggedIn { get; set; }

        public DateTime? lastLogin { get; set; }

        public UserStatus Status { get; set; }


        public List<Contact> Contacts { get; set; } = []; 

        public override string? ToString() => Email;

        public User() {
            active = true; //TODO: Must be set false once E]Email autentication is implemented
            Role = UserRole.Player;
            loggedIn = false;
            Status = UserStatus.Online;
        }
    }
}
