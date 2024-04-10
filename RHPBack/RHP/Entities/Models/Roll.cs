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
        public int Id { get; set; }
        public required HashSet<Dice> Dices { get; set; }
        public int Modifier { get; set; }
        public int Value { get; set; }
        public required Player Player { get; set; }
        public required Hall Hall { get; set; }

        public DateTimeOffset RolledAt { get; }

        public RollStatus Status { get; set; }

        public Roll()
        {
            RolledAt = DateTimeOffset.Now;
        }
    }


}
