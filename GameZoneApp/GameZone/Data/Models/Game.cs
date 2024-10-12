using GameZone.Common;
using Microsoft.EntityFrameworkCore;

namespace GameZone.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static ValidationConstants.Game;
    public class Game
    {
        [Key]
        [Comment("Unique identifier")]
        public int Id { get; set; }

        [Required]
        [MaxLength(TitleMaxLength)]
        [Comment("Game Title")]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(DescriptionMaxLength)]
        [Comment("Game description")]
        public string Description { get; set; } = null!;

        [Comment("Game image URL")]
        public string? ImageUrl { get; set; }

        [Required]
        [ForeignKey(nameof(Publisher))]
        [Comment("Game publisher identifier")]
        public string PublisherId { get; set; } = null!;

        public IdentityUser Publisher { get; set; } = null!;

        [Required]
        public DateTime ReleasedOn { get; set; }

        [Required]
        [Comment("The identifier of the game genre")]
        public int GenreId { get; set; }

        public Genre Genre { get; set; } = null!;

        public ICollection<GamerGame> GamersGames { get; set; } = new HashSet<GamerGame>();

        [Comment("Shows if game is deleted or not")]
        public bool IsDeleted { get; set; }

    }
}
