using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SeminarHub.Data.Models
{
    public class SeminarParticipant
    {
        [Required]
        public int SeminarId { get; set; }

        public Seminar Seminar { get; set; } = null!;

        [Required]
        public string ParticipantId { get; set; } = null!;

        public IdentityUser Participant { get; set; } = null!;
    }
}
