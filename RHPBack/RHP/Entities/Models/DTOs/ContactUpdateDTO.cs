namespace RHP.Entities.Models.DTOs
{
    public class ContactUpdateDTO
    {
        public string UserId { get; set; }
        public string Name { get; set; }        
        //Used by Contact Request
        public DateTime CreatedAt { get; set; }
    }
}
