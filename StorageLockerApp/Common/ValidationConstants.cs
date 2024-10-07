namespace StorageLocker.Common
{
    public static class ValidationConstants
    {
        public static class Customer
        {
            public const int CustomerFirstNameMinLength = 1;
            public const int CustomerFirstNameMaxLength = 85;
            public const int CustomerLastNameMinLength = 1;
            public const int CustomerLastNameMaxLength = 100;
        }

        public static class Location
        {
            public const int LocationNameMinLength = 3;
            public const int LocationNameMaxLength = 100;

            public const int LocationAddressMinLength = 5;
            public const int LocationAddressMaxLength = 150;

            public const string LocationPhoneNumberRegex = @"$[+\d{12}]^";

            public const int ManagerNameMinLength = 5;
            public const int ManagerNameMaxLength = 100;


            public const int DetailsMaxLength = 600;
        }

        public static class Bag
        {

            public const double BagMaxWeight = 35d;
            public const double BagMaxHeight = 60d;
            public const double BagMaxLength = 90d;
            public const double BagMaxDepth = 60d;

            public const int BagTypeMin = 1;
            public const int BagTypeMax = 6;

            public const int BagSizeMin = 1;
            public const int BagSizeMax = 5;
        }

    }
}
