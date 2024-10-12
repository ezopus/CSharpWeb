﻿namespace GameZone.Common
{
    public static class ValidationConstants
    {
        public static class Game
        {
            public const int TitleMinLength = 2;
            public const int TitleMaxLength = 50;
            public const int DescriptionMinLength = 10;
            public const int DescriptionMaxLength = 500;
            public const string ReleasedOnFormat = "yyyy-MM-dd";
        }

        public static class Genre
        {
            public const int GenreNameMinLength = 3;
            public const int GenreNameMaxLength = 25;
        }
    }
}
