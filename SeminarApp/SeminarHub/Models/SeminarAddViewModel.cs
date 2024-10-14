using SeminarHub.Data.Models;
using System.ComponentModel.DataAnnotations;
using static SeminarHub.Common.ErrorMessages;
using static SeminarHub.Common.ValidationConstants.SeminarValidation;

namespace SeminarHub.Models
{
    public class SeminarAddViewModel
    {
        [Required]
        [StringLength(TopicMaxLength, MinimumLength = TopicMinLength, ErrorMessage = ErrorSeminarTopic)]
        public string Topic { get; set; } = null!;

        [Required]
        [StringLength(LecturerMaxLength, MinimumLength = LecturerMinLength, ErrorMessage = ErrorSeminarLecturer)]
        public string Lecturer { get; set; } = null!;

        [Required]
        [StringLength(DetailsMaxLength, MinimumLength = DetailsMinLength, ErrorMessage = ErrorSeminarDetails)]
        public string Details { get; set; } = null!;

        [Range(DurationMinLength, DurationMaxLength, ErrorMessage = ErrorSeminarDuration)]
        public int Duration { get; set; }

        [Required]
        public string DateAndTime { get; set; } = null!;

        [Required]
        public int CategoryId { get; set; }

        public ICollection<Category> Categories { get; set; } = new List<Category>();

    }
}
