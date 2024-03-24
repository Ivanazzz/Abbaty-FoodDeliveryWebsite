namespace FoodDeliveryWebsite.Models.Constants
{
    public static class UserConstants
    {
        public const int FirstNameMaxLength = 20;
        public const int LastNameMaxLength = 20;

        public const string NameRegex = @"^[А-я\-]+$";
        public const string PasswordRegex = @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[.,#?!@$%^&*-]).{8,}$";
        public const string PhoneNumberRegex = @"^\+359\d{9}$";
        public const string EmailRegex = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+.[a-zA-Z]{2,}$";
    }
}
