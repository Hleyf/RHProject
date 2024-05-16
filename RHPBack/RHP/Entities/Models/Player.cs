using RHP.Entities.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace RHP.Entities.Models
{
    public class Player : IBaseEntity
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public int UserId { get; set; }
        public required User User { get; set; }
        public ICollection<Hall> Halls { get; set; }
        
        public Player()
        {
            Halls = new List<Hall>();
        }
        public override string? ToString() => Name;
    }
}
