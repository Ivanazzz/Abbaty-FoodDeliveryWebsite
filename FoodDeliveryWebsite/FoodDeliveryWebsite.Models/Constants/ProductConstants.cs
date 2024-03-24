namespace FoodDeliveryWebsite.Models.Constants
{
    public static class ProductConstants
    {
        public const int NameMaxLength = 50;
        public const int DescriptionMaxLength = 500;
        public const decimal PriceMinValue = 0.01m;
        public const int GramsMinValue = 1;
        public const int GramsMaxValue = 2001;
        public const int ImageNameMaxLength = 50;
        public const int ImageMimeTypeMaxLength = 30;

        public const string NameRegex = @"^[А-яA-z\s]+$";
        public const string DescriptionRegex = @"^[\(\)-.',А-яA-z1-9\s]+$";
    }
}
