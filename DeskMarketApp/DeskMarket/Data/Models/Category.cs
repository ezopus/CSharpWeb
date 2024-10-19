namespace DeskMarket.Data.Models
{
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;
    using static Common.ErrorMessages;
    using static Common.ValidationConstants.CategoryValidation;
    public class Category
    {
        [Key]
        [Comment("Unique category identifier")]
        public int Id { get; set; }

        [Required]
        [MaxLength(CategoryNameMaxLength, ErrorMessage = ErrorCategoryName)]
        [Comment("Unique category name.")]
        public string Name { get; set; } = null!;

        public ICollection<Product> Products { get; set; } = new List<Product>();

    }
}
