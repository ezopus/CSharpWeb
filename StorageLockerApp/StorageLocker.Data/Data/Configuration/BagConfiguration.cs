using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StorageLocker.Data.Enums;
using StorageLocker.Data.Models;

namespace StorageLocker.Data.Data.Configuration
{
    public class BagConfiguration : IEntityTypeConfiguration<Bag>
    {
        public void Configure(EntityTypeBuilder<Bag> builder)
        {
            builder
                .HasData(this.GenerateBags());
        }

        private IEnumerable<Bag> GenerateBags()
        {
            IEnumerable<Bag> customers = new List<Bag>()
            {
                new Bag()
                {
                    BagSize = Enum.Parse<BagSize>("Small"),
                    BagType = Enum.Parse<BagType>("Backpack"),
                    CustomerId = "37ffcbaf-96bf-4938-8f51-4c174844451f",
                },
                new Bag()
                {
                    BagSize = Enum.Parse<BagSize>("ExtraLarge"),
                    BagType = Enum.Parse<BagType>("Suitcase"),
                    CustomerId = "e1f8eb68-4899-4f1b-9b55-a6a467dfa398",
                },
                new Bag()
                {
                    BagSize = Enum.Parse<BagSize>("Medium"),
                    BagType = Enum.Parse<BagType>("Dufflebag"),
                    CustomerId = "fba496cd-628d-462a-beb8-0571b1a63eb3",
                }
            };

            return customers;
        }
    }
}
