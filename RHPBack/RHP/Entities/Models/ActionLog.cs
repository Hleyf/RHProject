using RHP.Entities.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace RHP.Entities.Models
{
    public class ActionLog : IBaseEntity
    {
        [Key]
        public int Id { get; set; }
        public required Player Player { get; set; }
        public required Hall Hall { get; set; }
        public required string Action { get; set; }
        public DateTimeOffset CreatedAt { get; }

        public ActionLog()
        {
            CreatedAt = DateTimeOffset.Now;
        }
    }
}