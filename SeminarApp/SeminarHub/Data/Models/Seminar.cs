using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static SeminarHub.Common.ErrorMessages;
using static SeminarHub.Common.ValidationConstants.SeminarValidation;
namespace SeminarHub.Data.Models
{
    public class Seminar
    {
        [Key]
        [Comment("Unique identifier of seminar")]
        public int Id { get; set; }

        [Required]
        [MaxLength(TopicMaxLength, ErrorMessage = ErrorSeminarTopic)]
        [Comment("Name of the seminar topic.")]
        public string Topic { get; set; } = null!;

        [Required]
        [MaxLength(LecturerMaxLength, ErrorMessage = ErrorSeminarLecturer)]
        [Comment("Lecturer name giving the seminar.")]
        public string Lecturer { get; set; } = null!;

        [Required]
        [MaxLength(DetailsMaxLength, ErrorMessage = ErrorSeminarDetails)]
        [Comment("Details for each seminar.")]
        public string Details { get; set; } = null!;

        [Required]
        [Comment("Unique identifier of each organizer.")]
        public string OrganizerId { get; set; } = null!;

        [ForeignKey(nameof(OrganizerId))]
        public IdentityUser Organizer { get; set; } = null!;

        [Required]
        [Comment("The start date and hour of the seminar.")]
        public DateTime DateAndTime { get; set; }

        [Range(DurationMinLength, DurationMaxLength, ErrorMessage = ErrorSeminarDuration)]
        [Comment("The duration of each seminar in minutes.")]
        public int Duration { get; set; }

        [Required]
        [Comment("The category identifier for each seminar.")]
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;

        public ICollection<SeminarParticipant> SeminarsParticipants { get; set; } = new List<SeminarParticipant>();

        public bool IsDeleted { get; set; }
    }
}
