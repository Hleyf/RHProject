public class PlayerDTO
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public required string Name { get; set; }
    public int[]? HallIds { get; set; }
    public required string Email { get; set; }
}
