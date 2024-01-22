using RHP.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace RHP.Models
{

    public enum RollStatus
    {
        Success,
        Failure,
        CriticalSuccess,
        CriticalFailure
    }
    public class Roll : IBase
    {
        public int Id => RollId;
        [Key]
        public int RollId { get; set; }
        public required HashSet<Dice> Dices { get; set; }
        public int Modifier { get; set; }
        public int Value { get; set; }

        public RollStatus Status { get; set; }
    }
}
