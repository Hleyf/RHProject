using RHP.Entities.Interfaces;

namespace RHP.Entities.Models
{
    public class User : IBaseEntity
    {
        public int Id => UserId;
        public int UserId { get; set; }
        public required string Email { get; set; }

        public required string Password { get; set; }

        public override string? ToString() => Email;
    }
}
