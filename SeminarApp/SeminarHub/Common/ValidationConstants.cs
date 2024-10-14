namespace SeminarHub.Common
{
    public static class ValidationConstants
    {
        public const string RequiredDateFormat = "dd/MM/yyyy HH:mm";
        public static class SeminarValidation
        {
            public const int TopicMinLength = 3;
            public const int TopicMaxLength = 100;
            public const int LecturerMinLength = 5;
            public const int LecturerMaxLength = 60;
            public const int DetailsMinLength = 10;
            public const int DetailsMaxLength = 500;
            public const int DurationMinLength = 30;
            public const int DurationMaxLength = 180;
        }

        public static class CategoryValidation
        {
            public const int CategoryNameMinLength = 3;
            public const int CategoryNameMaxLength = 50;
        }
    }
}
