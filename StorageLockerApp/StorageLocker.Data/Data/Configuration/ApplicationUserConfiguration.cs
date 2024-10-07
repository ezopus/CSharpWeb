using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StorageLocker.Data.Models;

namespace StorageLocker.Data.Data.Configuration
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder
                .HasData(this.GenerateCustomers());
        }

        private IEnumerable<ApplicationUser> GenerateCustomers()
        {
            IEnumerable<ApplicationUser> customers = new List<ApplicationUser>()
            {
                new ApplicationUser()
                {
                    FirstName = "Georgi",
                    LastName = "Kovachev",
                    Email = "georgi@kovachev.com",
                    PhoneNumber = "+359888123456"
                },
                new ApplicationUser()
                {
                    FirstName = "Elena",
                    LastName = "Kovacheva",
                    Email = "elena@kovacheva.com",
                    PhoneNumber = "+359877554423"
                },
                new ApplicationUser()
                {
                    FirstName = "Genadii",
                    LastName = "Krokodilov",
                }
            };

            return customers;
        }
    }
}
