using RHP.Entities.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace RHP.Entities.Models
{
    public class Hall : IBaseEntity
    {
        [Key]
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public required Player GameMaster { get; set; }
        public required Player[] Players { get; set; }
        public Roll[]? Rolls { get; set; }
        public override string? ToString() => Title;

    }
}
