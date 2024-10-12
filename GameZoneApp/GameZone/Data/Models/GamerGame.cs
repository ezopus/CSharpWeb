using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameZone.Data.Models
{
    [PrimaryKey(nameof(GameId), nameof(GamerId))]
    public class GamerGame
    {
        [Required]
        [Comment("Unique game identifier")]
        public int GameId { get; set; }

        [ForeignKey(nameof(GameId))]
        public Game Game { get; set; } = null!;

        [Required]
        [Comment("Unique gamer identifier")]
        public string GamerId { get; set; } = null!;

        [ForeignKey(nameof(GamerId))]
        public IdentityUser Gamer { get; set; } = null!;

    }
}
