using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static SoftUniBazar.Common.ValidationConstants.CategoryValidations;

namespace SoftUniBazar.Data.Models
{
    public class Category
    {
        [Key]
        [Comment("Unique identifier of category.")]
        public int Id { get; set; }

        [Required]
        [MaxLength(CategoryNameMaxLength)]
        [Comment("Name of category.")]
        public string Name { get; set; } = null!;

        public ICollection<Ad> Ads { get; set; } = new List<Ad>();
    }
}
