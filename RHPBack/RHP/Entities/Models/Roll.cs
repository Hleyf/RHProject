using RHP.Entities.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace RHP.Entities.Models
{

    public enum RollStatus
    {
        Success,
        Failure,
        CriticalSuccess,
        CriticalFailure
    }
    public class Roll : IBaseEntity
    {
        [Key]
        public int id { get; set; }
        public required HashSet<Dice> dices { get; set; }
        public int modifier { get; set; }
        public int value { get; set; }
        public required Player player { get; set; }
        public required Hall hall { get; set; }

        public DateTimeOffset rolledAt { get; }

        public RollStatus status { get; set; }

        public Roll()
        {
            rolledAt = DateTimeOffset.Now;
        }
    }


}
