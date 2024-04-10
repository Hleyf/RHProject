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
        public ICollection<Player> Players { get; set; }
        public ICollection<Roll> Rolls { get; set; }

        public Hall()
        {
            Players = new List<Player>();
            Rolls = new List<Roll>();
        }

        public override string? ToString() => Title;
    }
}
