using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static SeminarHub.Common.ErrorMessages;
using static SeminarHub.Common.ValidationConstants.CategoryValidation;

namespace SeminarHub.Data.Models
{
    public class Category
    {
        [Key]
        [Comment("Unique identifier for each category.")]
        public int Id { get; set; }

        [Required]
        [MaxLength(CategoryNameMaxLength, ErrorMessage = ErrorCategoryName)]
        public string Name { get; set; } = null!;

        public ICollection<Seminar> Seminars { get; set; } = new List<Seminar>();
    }
}
