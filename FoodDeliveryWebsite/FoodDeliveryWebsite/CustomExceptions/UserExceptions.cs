namespace FoodDeliveryWebsite.CustomExceptions
{
    public class UserExceptions : Exception
    {
        public UserExceptions(string message) 
            : base(message)
        {
        }
    }
}
