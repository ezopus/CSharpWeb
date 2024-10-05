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

			public const string ReleaseDateFormat = "MM/dddd";
		}

		public static class Cinema
		{
			public const int CinemaNameMinLength = 2;
			public const int CinemaNameMaxLength = 30;

			public const int CinemaLocationMinLength = 2;
			public const int CinemaLocationMaxLength = 86;
		}
	}
}
