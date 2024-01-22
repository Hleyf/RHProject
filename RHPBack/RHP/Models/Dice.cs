using RHP.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace RHP.Models
{
    public enum DiceType
    {
        D4,
        D6,
        D8,
        D10,
        D12,
        D20,
        D100
    }
    public class Dice : IBase
    {
        public int Id => DiceId;
        [Key]
        public int DiceId { get; set; }
        public DiceType Type { get; set; }
        public int Value { get; set; }
    }
}
