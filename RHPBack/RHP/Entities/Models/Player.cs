using System.ComponentModel.DataAnnotations;

namespace RHP.Entities.Models
{
    public class Player : User
    {

        [Key]
        public int PlayerId { get; set; }
        public required string Name { get; set; }

        public Hall[]? Halls { get; set; }
        public override string? ToString() => Name;

    }
}
