namespace RHP.Entities.Models.DTOs
{
    public class HallDTO
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int gameMasterId { get; set; }
        public IEnumerable<PlayerDTO> players { get; set; }

    }
}