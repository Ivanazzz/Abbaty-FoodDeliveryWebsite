namespace FoodDeliveryWebsite.Repositories.CustomExceptionMessages
{
    public static class ExceptionMessages
    {
        // Address
        public const string InvalidAddress = "Invalid address";

        // Discount
        public const string StartExpirationDateRequired = "Start date and Expiration date are required";
        public const string StartDateGreaterThanExpirationDate = "Start date should not be greater than Expiration date";

        // Order
        public const string InvalidOrder = "Invalid order";

        // OrderItem
        public const string InvalidOrderItem = "Invalid order item";
        public const string InvalidOrderItemForUser = "Invalid order item for user";

        // Product
        public const string InvalidProduct = "Invalid product";
        public const string ProductQuantityLessThan1 = "Product quantity must be grater than 0";

        // User
        public const string InvalidUser = "Invalid user";
        public const string AlreadyExistingUser = "User with the given email already exists";
        public const string NonExistentUser = "There isn't user with the given email";
        public const string InvalidUserPassword = "Invalid password";
    }
}
