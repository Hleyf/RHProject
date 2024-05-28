namespace RHP.Entities.Models.DTOs
{
    public class Contact
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool loggedIn { get; set; }
        public UserStatus Status { get; set; }
        public DateTime lastLoggedIn { get; set; }
    }
}
