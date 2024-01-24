using RHP.Interfaces;

namespace RHP.Models
{
    public class User : IBase
    {
        public int Id => UserId;
        public int UserId { get; set; }
        public required string Email { get; set; }

        public required string Password { get; set; }
        public override string? ToString() => Email;
    }
}
