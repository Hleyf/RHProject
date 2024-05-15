using RHP.Entities.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace RHP.Entities.Models
{
    public class ActionLog : IBaseEntity
    {
        [Key]
        public int id { get; set; }
        public required Player player { get; set; }
        public required Hall hall { get; set; }
        public required string action { get; set; }
        public DateTimeOffset createdAt { get; }

        public ActionLog()
        {
            createdAt = DateTimeOffset.Now;
        }
    }
}