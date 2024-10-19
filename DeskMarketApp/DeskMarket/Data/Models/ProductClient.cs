namespace DeskMarket.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ProductClient
    {
        [Required]
        [Comment("Unique product identifier.")]
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }

        public Product Product { get; set; } = null!;

        [Required]
        [Comment("Unique client identifier.")]
        [ForeignKey(nameof(Client))]
        public string ClientId { get; set; } = null!;

        public IdentityUser Client { get; set; } = null!;

    }
}
