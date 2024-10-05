﻿namespace CinemaApp.Web.ViewModels.Movie
{
	using System.ComponentModel.DataAnnotations;
	using static Common.EntityValidationConstants.Movie;
	using static Common.EntityValidationMessages.Movie;
	public class AddMovieInputModel
	{
		public AddMovieInputModel()
		{
			this.ReleaseDate = DateTime.UtcNow.ToString(ReleaseDateFormat);
		}
		[Required(ErrorMessage = TitleRequiredMessage)]
		[MinLength(TitleMinLength)]
		[MaxLength(TitleMaxLength)]
		public string Title { get; set; } = null!;


		[Required(ErrorMessage = GenreRequiredMessage)]
		[MinLength(GenreMinLength)]
		[MaxLength(GenreMaxLength)]
		public string Genre { get; set; } = null!;

		[Required(ErrorMessage = ReleaseDateRquiredMessage)]
		public string ReleaseDate { get; set; }

		[Required(ErrorMessage = DurationRequiredMessage)]
		[Range(DurationMinValue, DurationMaxValue)]
		public int Duration { get; set; }


		[Required(ErrorMessage = DirectorRequiredMessage)]
		[MinLength(DirectorMinLength)]
		[MaxLength(DirectorMaxLength)]
		public string Director { get; set; } = null!;

		[Required]
		[MaxLength(DescriptionMaxLength)]
		public string Description { get; set; } = null!;

	}
}
