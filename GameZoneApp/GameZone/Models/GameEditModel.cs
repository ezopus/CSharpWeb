using GameZone.Data.Models;

namespace GameZone.Models
{
    public class GameEditModel
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? ImageUrl { get; set; }
        public required int GenreId { get; set; }
        public List<Genre> Genres { get; set; } = new List<Genre>();
        public required string ReleasedOn { get; set; }
        public required string Description { get; set; }
    }
}
