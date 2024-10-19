namespace DeskMarket.Common
{
    public static class ErrorMessages
    {
        public const string ErrorDateFormat = "Date must be in following format: dd-MM-yyyy";

        public const string ErrorProductName = "Product name must be between 2 and 60 symbols.";
        public const string ErrorProductDescription = "Product description must be between 10 and 250 symbols.";
        public const string ErrorProductPrice = "Price must be in range (1 - 3000).";

        public const string ErrorCategoryName = "Category name must be between 3 and 20 symbols.";
    }
}
