namespace CinemaApp.Web.ViewModels.Movie
{
	using System.ComponentModel.DataAnnotations;
	using static Common.EntityValidationConstants.Movie;
	public class AddMovieInputModel
	{
		[Required]
		[MinLength(TitleMinLength)]
		[MaxLength(TitleMaxLength)]
		public string Title { get; set; } = null!;


		[Required]
		[MinLength(GenreMinLength)]
		[MaxLength(GenreMaxLength)]
		public string Genre { get; set; } = null!;

		public string ReleaseDate { get; set; } = null!;

		[Required]
		[Range(DurationMinValue, DurationMaxValue)]
		public int Duration { get; set; }


		[Required]
		[MinLength(DirectorMinLength)]
		[MaxLength(DirectorMaxLength)]
		public string Director { get; set; } = null!;

		[Required]
		[MaxLength(DescriptionMaxLength)]
		public string Description { get; set; } = null!;

	}
}
