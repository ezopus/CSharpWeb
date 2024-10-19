namespace DeskMarket.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static Common.ErrorMessages;
    using static Common.ValidationConstants.ProductValidation;
    public class Product
    {
        [Key]
        [Comment("Unique identifier of product.")]
        public int Id { get; set; }

        [Required]
        [MaxLength(ProductNameMaxLength, ErrorMessage = ErrorProductName)]
        [Comment("The name of the product.")]
        public string ProductName { get; set; } = null!;

        [Required]
        [MaxLength(DescriptionMaxLength, ErrorMessage = ErrorProductDescription)]
        [Comment("Description of product.")]
        public string Description { get; set; } = null!;

        [Required]
        [Comment("Price of the product.")]
        public decimal Price { get; set; }

        [Comment("Url with a picture of the product.")]
        public string? ImageUrl { get; set; }

        [Required]
        [Comment("Unique identifier of seller of product.")]
        [ForeignKey(nameof(Seller))]
        public string SellerId { get; set; } = null!;

        public IdentityUser Seller { get; set; } = null!;

        [Required]
        [Comment("Date and time stamp when product is added to database.")]
        public DateTime AddedOn { get; set; }

        [Required]
        [Comment("Unique identifier of product category, foreign key to table Categories.")]
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        public Category Category { get; set; } = null!;

        [Comment("Flag for indicating if product is deleted or not.")]
        public bool IsDeleted { get; set; } = false;

        public ICollection<ProductClient> ProductsClients { get; set; } = new List<ProductClient>();

    }
}
