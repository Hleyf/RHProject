using RHP.Entities.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace RHP.Entities.Models
{
    public class Player : IBaseEntity
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public required User User { get; set; }
        public Hall[]? Halls { get; set; }

        public override string? ToString() => Name;
    }
}
