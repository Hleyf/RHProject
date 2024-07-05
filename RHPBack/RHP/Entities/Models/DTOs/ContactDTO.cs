namespace RHP.Entities.Models.DTOs
{
    public class ContactDTO
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool loggedIn { get; set; }
        public UserStatus userStatus { get; set; }
        public ContactStatus status { get; set; }
        public DateTime lastLogin { get; set; }
    }
}
