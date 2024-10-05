using CinemaApp.Common;
using CinemaApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaApp.Data.Configuration
{
	public class CinemaConfiguration : IEntityTypeConfiguration<Cinema>
	{
		public void Configure(EntityTypeBuilder<Cinema> builder)
		{
			builder
				.HasKey(c => c.Id);

			builder
				.Property(c => c.Name)
				.IsRequired()
				.HasMaxLength(EntityValidationConstants.Cinema.CinemaNameMaxLength);

			builder
				.Property(c => c.Location)
				.IsRequired()
				.HasMaxLength(EntityValidationConstants.Cinema.CinemaLocationMaxLength);

			builder
				.HasData(GenerateCinemas());
		}

		private IEnumerable<Cinema> GenerateCinemas()
		{
			IEnumerable<Cinema> cinemas = new List<Cinema>()
			{
				new Cinema()
				{
					Name = "Cinema City",
					Location = "Plovdiv"
				},
				new Cinema()
				{
					Name = "Eccoplex",
					Location = "Plovdiv"
				},
				new Cinema()
				{
					Name = "Arena",
					Location = "Plovdiv"
				}
			};

			return cinemas;
		}
	}
}
