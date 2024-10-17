using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static SoftUniBazar.Common.ValidationConstants.AdValidations;

namespace SoftUniBazar.Data.Models
{
    public class Ad
    {
        [Key]
        [Comment("Unique identifier of the ad.")]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        [Comment("Name or title of the ad.")]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(DescriptionMaxLength)]
        [Comment("Description of the ad.")]
        public string Description { get; set; } = null!;

        [Required]
        [Comment("Price of the ad in decimal format.")]
        [DataType("MONEY")]
        public decimal Price { get; set; }

        [Required]
        [Comment("Unique user id of ad owner/poster.")]
        [ForeignKey(nameof(Owner))]
        public string OwnerId { get; set; } = null!;

        public IdentityUser Owner { get; set; } = null!;

        [Required]
        [Comment("Image url of picture used for ad.")]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [Comment("Date and time the ad was created on.")]
        public DateTime CreatedOn { get; set; }

        [Required]
        [Comment("Unique identifier of ad category.")]
        public int CategoryId { get; set; }

        public Category Category { get; set; } = null!;

        public ICollection<AdBuyer> AdsBuyers { get; set; } = new List<AdBuyer>();

        [Comment("Boolean flag to check if ad is deleted or not.")]
        public bool IsDeleted { get; set; } = false;
    }
}
