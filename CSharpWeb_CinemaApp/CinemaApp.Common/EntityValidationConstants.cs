namespace CinemaApp.Common
{
	public static class EntityValidationConstants
	{
		public static class Movie
		{
			public const int TitleMinLength = 1;
			public const int TitleMaxLength = 80;

			public const int GenreMinLength = 3;
			public const int GenreMaxLength = 20;

			public const int DirectorMinLength = 2;
			public const int DirectorMaxLength = 100;

			public const int DescriptionMaxLength = 500;

			public const int DurationMinValue = 1;
			public const int DurationMaxValue = 999;
		}
	}
}
