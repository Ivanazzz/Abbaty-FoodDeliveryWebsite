namespace FoodDeliveryWebsite.CustomExceptions
{
    public class UserExceptions : Exception
    {
        public const string InvalidUser = "Invalid user";

        public UserExceptions(string message) 
            : base(message)
        {
        }
    }
}
