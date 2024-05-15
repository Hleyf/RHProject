using RHP.Entities.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace RHP.Entities.Models
{
    public class Player : IBaseEntity
    {
        [Key]
        public int id { get; set; }
        public required string name { get; set; }
        public int userId { get; set; }
        public required User user { get; set; }
        public ICollection<Hall> halls { get; set; }
        
        public Player()
        {
            halls = new List<Hall>();
        }
        public override string? ToString() => name;
    }
}
