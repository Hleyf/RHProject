using RHP.Entities.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace RHP.Entities.Models
{
    public enum ContactRequestStatus
    {
        Pending,
        Accepted,
        Rejected
    }

    public class ContactRequest : IBaseEntity
    {
        [Key]
        public int Id { get; set; }
        public User From { get; set; }
        public User To { get; set; }
        public ContactRequestStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
