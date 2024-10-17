namespace SoftUniBazar.Common
{
    public static class ValidationConstants
    {
        public const string RequiredDateFormat = "yyyy-MM-dd H:mm";
        public class AdValidations
        {
            public const int NameMinLength = 5;
            public const int NameMaxLength = 25;
            public const int DescriptionMinLength = 15;
            public const int DescriptionMaxLength = 250;
        }

        public class CategoryValidations
        {
            public const int CategoryNameMinLength = 3;
            public const int CategoryNameMaxLength = 15;
        }
    }
}
