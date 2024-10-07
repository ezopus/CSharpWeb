using StorageLocker.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StorageLocker.Data.Models
{
    public class Bag
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public BagType BagType { get; set; }

        [Required]
        public BagSize BagSize { get; set; }

        [Required]
        [ForeignKey(nameof(Customer))]
        public string CustomerId { get; set; } = null!;

        public ApplicationUser Customer { get; set; } = null!;
    }
}
