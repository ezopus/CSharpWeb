using System.ComponentModel.DataAnnotations;
using static StorageLocker.Common.ValidationConstants.Location;

namespace StorageLocker.Data.Models
{
    public class Location
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(LocationNameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(LocationAddressMaxLength)]
        public string Address { get; set; } = null!;

        [Required]
        [RegularExpression(LocationPhoneNumberRegex)]
        public string PhoneNumber { get; set; } = null!;

        [MaxLength(ManagerNameMaxLength)]
        public string? ManagerName { get; set; }

        [Required]
        public bool HasFreeLockers { get; set; }

        [MaxLength(DetailsMaxLength)]
        public string? Details { get; set; }
    }
}
