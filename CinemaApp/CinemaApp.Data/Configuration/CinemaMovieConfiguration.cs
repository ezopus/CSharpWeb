﻿using CinemaApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaApp.Data.Configuration
{
	public class CinemaMovieConfiguration : IEntityTypeConfiguration<CinemaMovie>
	{
		public void Configure(EntityTypeBuilder<CinemaMovie> builder)
		{
			builder
				.HasKey(cm => new { cm.MovieId, cm.CinemaId });

			builder
				.HasOne(cm => cm.Movie)
				.WithMany(m => m.MovieCinemas)
				.HasForeignKey(cm => cm.MovieId)
				.OnDelete(DeleteBehavior.Restrict);

			builder
				.HasOne(cm => cm.Cinema)
				.WithMany(c => c.CinemaMovies)
				.HasForeignKey(c => c.CinemaId)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
