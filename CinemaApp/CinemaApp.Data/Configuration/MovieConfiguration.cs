using CinemaApp.Common;
using CinemaApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaApp.Data.Configuration
{
	public class MovieConfiguration : IEntityTypeConfiguration<Movie>
	{
		public void Configure(EntityTypeBuilder<Movie> builder)
		{
			//Fluent API
			builder.HasKey(m => m.Id);

			builder
				.Property(m => m.Title)
				.IsRequired()
				.HasMaxLength(EntityValidationConstants.Movie.TitleMaxLength);

			builder
				.Property(m => m.Genre)
				.IsRequired()
				.HasMaxLength(EntityValidationConstants.Movie.GenreMaxLength);

			builder
				.Property(m => m.Director)
				.IsRequired()
				.HasMaxLength(EntityValidationConstants.Movie.DirectorMaxLength);

			builder
				.Property(m => m.Description)
				.IsRequired()
				.HasMaxLength(EntityValidationConstants.Movie.DescriptionMaxLength);

			builder.HasData(this.SeedMovies());
		}

		private List<Movie> SeedMovies()
		{
			List<Movie> movies = new List<Movie>()
			{
				new Movie()
				{
					Title = "Harry Potter and the Philosopher's Stone",
					Genre  = "Fantasy",
					ReleaseDate = new DateTime(2002, 02, 15),
					Director = "Chris Columbus",
					Duration = 157,
					Description = "Harry Potter, an eleven-year-old orphan, discovers that he is a wizard and is invited to study at Hogwarts. Even as he escapes a dreary life and enters a world of magic, he finds trouble awaiting him."
				},
				new Movie()
				{
					Title = "Harry Potter and the Chamber of Secrets",
					Genre  = "Fantasy",
					ReleaseDate = new DateTime(2002, 11, 03),
					Director = "Chris Columbus",
					Duration = 161,
					Description = "Harry Potter and the Chamber of Secrets is a 2002 fantasy film directed by Chris Columbus from a screenplay by Steve Kloves. It is based on the 1998 novel Harry Potter and the Chamber of Secrets by J. K. Rowling. The story follows Harry's second year at Hogwarts School of Witchcraft and Wizardry, where the Heir of Salazar Slytherin opens the Chamber of Secrets, unleashing a monster that petrifies the school's students."
				},
				new Movie()
				{
					Title = "The Lord of the Rings: The Fellowship of the Ring",
					Genre  = "Fantasy",
					ReleaseDate = new DateTime(2001, 12, 10),
					Director = "Peter Jackson",
					Duration = 178,
					Description = "Set in Middle-earth, the story tells of the Dark Lord Sauron, who seeks the One Ring, which contains part of his might, to return to power. The Ring has found its way to the young hobbit Frodo Baggins. The fate of Middle-earth hangs in the balance as Frodo and eight companions (who form the Company of the Ring) begin their perilous journey to Mount Doom in the land of Mordor, the only place where the Ring can be destroyed."
				}
			};

			return movies;
		}
	}
}
