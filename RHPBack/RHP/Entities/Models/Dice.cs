﻿using RHP.Entities.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace RHP.Entities.Models
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
    public class Dice : IBaseEntity
    {
        [Key]
        public int Id { get; set; }
        public DiceType Type { get; set; }
        public int Value { get; set; }
    }
}
