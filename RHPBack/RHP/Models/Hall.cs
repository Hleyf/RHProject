﻿using RHP.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace RHP.Models
{
    public class Hall : IBase
    {
        public int Id => HallId;
        [Key]
        public int HallId { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public required Player GameMaster { get; set; }
        public required Player[] Players { get; set; }
        public Roll[]? Rolls { get; set; }
        public override string? ToString() => Title;
        
    }
}
