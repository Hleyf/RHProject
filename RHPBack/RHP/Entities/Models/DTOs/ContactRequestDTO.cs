namespace RHP.Entities.Models.DTOs
{
    public class ContactRequestDTO
    {
        public string FromUserId { get; set; }
        public string ToUserId { get; set; }
        public ContactRequestStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }


    }
}
