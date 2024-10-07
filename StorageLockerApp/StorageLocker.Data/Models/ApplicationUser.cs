using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using static StorageLocker.Common.ValidationConstants.Customer;

namespace StorageLocker.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(CustomerFirstNameMaxLength)]
        public string? FirstName { get; set; }

        [MaxLength(CustomerLastNameMaxLength)]
        public string? LastName { get; set; }
    }
}
