using RHP.Entities.Interfaces;

namespace RHP.Entities.Models
{
    public enum ContactStatus
    {
        Pending,
        Accepted,
        Rejected,
        Blocked
    }
    public class Contact : IBaseEntity
    {
        public int Id { get; set; }
        public User Requestor { get; set; }
        public User Recipient { get; set; }
        public ContactStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
