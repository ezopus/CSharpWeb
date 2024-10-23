using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StorageLocker.Data.Models
{
    public class Locker
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [ForeignKey(nameof(Bag))]
        public Guid BagId { get; set; }
        public Bag Bag { get; set; }

        [Required]
        [ForeignKey(nameof(Location))]
        public Guid LocationId { get; set; }

        public Location Location { get; set; }

    }
}
