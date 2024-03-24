namespace FoodDeliveryWebsite.Models.Constants
{
    public static class AddressConstants
    {
        public const int CityMaxLength = 20;
        public const int StreetMaxLength = 30;
        public const int StreetNoMinValue = 1;
        public const int FloorMinValue = 0;
        public const int ApartmentNoMinValue = 1;

        public const string CityRegex = @"^[А-я\s]+$";
        public const string StreetRegex = @"^[А-я\s]+$";
    }
}
