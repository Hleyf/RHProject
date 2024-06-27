namespace RHP.Entities.Models.DTOs
{
    public class ContactRequestDTO
    {
        public int Id { get; set; }
        public string FromUserId { get; set; }
        public string ToUserId { get; set; }
        public ContactStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }


    }
}
