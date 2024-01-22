using RHP.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace RHP.Models
{
    public class Hall : IBase
    {
        public int Id => HallId;
        [Key]
        public int HallId { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public int GameMasterId { get; set; }
        public required int[] PlayerIds { get; set; }
        public int[]? RollIds { get; set; }
        public override string? ToString() => Title;
        
    }
}
