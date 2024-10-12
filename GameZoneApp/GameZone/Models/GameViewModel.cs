namespace GameZone.Models
{
    using GameZone.Data.Models;
    using System.ComponentModel.DataAnnotations;
    using static GameZone.Common.ValidationConstants.Game;
    public class GameViewModel
    {
        [Required]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
        public string Title { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string ReleasedOn { get; set; } = DateTime.Today.ToString(ReleasedOnFormat);

        [Required]
        public int GenreId { get; set; }

        public List<Genre> Genres { get; set; } = new List<Genre>();
    }
}
