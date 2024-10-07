using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StorageLocker.Data.Models;

namespace StorageLocker.Data.Data.Configuration
{
    public class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder
                .HasData(this.GenerateLocations());

        }

        private IEnumerable<Location> GenerateLocations()
        {
            IEnumerable<Location> customers = new List<Location>()
            {
                new Location()
                {
                    Name = "Leksi",
                    PhoneNumber = "+359888123321",
                    Address = "ul. Maria Luiza 32",
                    HasFreeLockers = true,
                },
                new Location()
                {
                    Name = "Billa",
                    PhoneNumber = "+359888188842",
                    Address = "ul. Rayko Daskalov 103" ,
                    HasFreeLockers = true,
                },
                new Location()
                {
                    Name = "Store Bags Here",
                    PhoneNumber = "+359898977977",
                    Address = "pl. Centralen 1",
                    HasFreeLockers = false,
                }
            };

            return customers;
        }
    }
}
