public class PlayerDTO
{
    public int id { get; set; }
    public int userId { get; set; }
    public required string name { get; set; }
    public int[]? hallIds { get; set; }
    public required string email { get; set; }
}
