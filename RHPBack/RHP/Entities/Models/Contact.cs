using RHP.Entities.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace RHP.Entities.Models
{
    public class Contact : IBaseEntity
    {
        [Key]
        public int Id { get; set; }
        public Player Player { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

    }
}
