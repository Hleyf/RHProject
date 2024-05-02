namespace RHP.Entities.Models.DTOs
{
    public class HallDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int GameMasterId { get; set; }
        public int NumberOfPlayers { get; set; }

    }
}