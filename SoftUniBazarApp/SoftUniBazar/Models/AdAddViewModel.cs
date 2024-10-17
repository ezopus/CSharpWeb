using SoftUniBazar.Data.Models;
using System.ComponentModel.DataAnnotations;
using static SoftUniBazar.Common.ErrorMessages;
using static SoftUniBazar.Common.ValidationConstants.AdValidations;

namespace SoftUniBazar.Models
{
    public class AdAddViewModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = ErrorAdName)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength, ErrorMessage = ErrorAdDescription)]
        public string Description { get; set; } = null!;

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string ImageUrl { get; set; } = null!;

        [Required]
        public int CategoryId { get; set; }

        public ICollection<Category> Categories { get; set; } = new List<Category>();
    }
}
