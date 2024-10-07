using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StorageLocker.Data.Data.Configuration;
using StorageLocker.Data.Models;

namespace StorageLocker.Data.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            var userConfig = new ApplicationUserConfiguration();
            var locationConfig = new LocationConfiguration();
            var bagConfing = new BagConfiguration();

            builder.ApplyConfiguration(userConfig);
            builder.ApplyConfiguration(locationConfig);
            builder.ApplyConfiguration(bagConfing);
        }

        public DbSet<ApplicationUser> Customers { get; set; }

        public DbSet<Bag> Bags { get; set; }
        public DbSet<Location> Locations { get; set; }

    }
}
