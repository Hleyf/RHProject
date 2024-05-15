using RHP.Entities.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace RHP.Entities.Models
{
    public class Hall : IBaseEntity
    {
        [Key]
        public int id { get; set; }
        public required string title { get; set; }
        public string? description { get; set; }
        public required Player gameMaster { get; set; }
        public ICollection<Player> players { get; set; }
        public List<Roll> rolls { get; set; }

        public Hall()
        {
            players = [];
            rolls = new List<Roll>();
        }

        public override string? ToString() => title;
    }
}
