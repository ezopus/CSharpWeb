namespace Homies.Common
{
    public static class ValidationConstants
    {
        public const string RequiredDateFormat = "yyyy-MM-dd H:mm";
        public static class Event
        {
            public const int EventNameMinLength = 5;
            public const int EventNameMaxLength = 20;
            public const int DescriptionNameMinLength = 15;
            public const int DescriptionNameMaxLength = 150;
        }

        public static class Type
        {
            public const int TypeNameMinLength = 5;
            public const int TypeNameMaxLength = 15;
        }
    }
}
