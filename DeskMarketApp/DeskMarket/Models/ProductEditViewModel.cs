namespace DeskMarket.Models
{
    using DeskMarket.Data.Models;
    using System.ComponentModel.DataAnnotations;
    using static Common.ErrorMessages;
    using static Common.ValidationConstants.ProductValidation;
    public class ProductEditViewModel
    {
        [Required]
        [StringLength(ProductNameMaxLength, MinimumLength = ProductNameMinLength, ErrorMessage = ErrorProductName)]
        public string ProductName { get; set; } = null!;

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength, ErrorMessage = ErrorProductDescription)]
        public string Description { get; set; } = null!;

        [Required]
        [Range(PriceMinRange, PriceMaxRange, ErrorMessage = ErrorProductPrice)]
        public decimal Price { get; set; }

        public string? ImageUrl { get; set; }

        [Required]
        public string AddedOn { get; set; } = null!;

        [Required]
        public int CategoryId { get; set; }

        public ICollection<Category> Categories { get; set; } = new List<Category>();

        public string SellerId { get; set; } = null!;
    }
}
