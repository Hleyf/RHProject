using RHP.Entities.Interfaces;

namespace RHP.Entities.Models
{
    public class ActionLog : IBaseEntity
    {
        public int Id => ActionLogId;
        public int ActionLogId { get; set; }
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