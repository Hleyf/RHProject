using RHP.Entities.Models;
using RHP.Entities.Models.DTOs;

public class PlayerDTO
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public bool loggedIn { get; set; }
    public UserStatus Status { get; set; }
    public DateTime lastLoggedIn { get; set; }
    public required string Name { get; set; }
    public int[]? HallIds { get; set; }
    public List<ContactDTO> Contacts { get; set; }
    public required string Email { get; set; }
}
