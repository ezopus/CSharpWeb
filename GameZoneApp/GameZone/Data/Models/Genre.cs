
using GameZone.Common;

namespace GameZone.Data.Models
{
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;
    using static ValidationConstants.Genre;
    public class Genre
    {
        [Key]
        [Comment("The genre unique identifier")]
        public int Id { get; set; }

        [Required]
        [MaxLength(GenreNameMaxLength)]
        [Comment("The genre name")]
        public string Name { get; set; } = null!;

        public ICollection<Game> Games { get; set; } = new HashSet<Game>();
    }
}
