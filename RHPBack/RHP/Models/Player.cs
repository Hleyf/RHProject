using RHP.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace RHP.Models
{
    public class Player : IBase
    {
        public int Id => PlayerId;
        [Key]
        public int PlayerId { get; set; }
        public required string Name { get; set; }
        public bool IsGameMaster { get; set; } = false;

        public Hall? Hall { get; set; }
        public override string? ToString() => Name;

    }
}
