namespace DeskMarket.Common
{
    public static class ValidationConstants
    {
        public const string RequiredDateFormat = "dd-MM-yyyy";
        public static class ProductValidation
        {
            public const int ProductNameMinLength = 2;
            public const int ProductNameMaxLength = 60;
            public const int DescriptionMinLength = 10;
            public const int DescriptionMaxLength = 250;
            public const double PriceMinRange = 1.00;
            public const double PriceMaxRange = 3000.00;
        }

        public static class CategoryValidation
        {
            public const int CategoryNameMinLength = 3;
            public const int CategoryNameMaxLength = 20;
        }
    }
}
