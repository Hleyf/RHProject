using RHP.Entities.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace RHP.Entities.Models
{
    public class Player : IBaseEntity
    {
        public int Id => PlayerId;
        [Key]
        public int PlayerId { get; set; }
        public required string Name { get; set; }
        public required User User { get; set; }
        public Hall[]? Halls { get; set; }

        public override string? ToString() => Name;
    }
}
