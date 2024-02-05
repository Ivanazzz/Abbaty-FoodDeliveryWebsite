namespace FoodDeliveryWebsite.Repositories.CustomExceptionMessages
{
    public static class ExceptionMessages
    {
        // Address
        public const string InvalidAddress = "Невалиден адрес";

        // Discount
        public const string StartExpirationDateRequired = "Датата на стартиране и датата на изтичане са задължителни";
        public const string StartDateGreaterThanExpirationDate = "Датата на стартиране не трябва да бъде след датата на приключване";

        // Order
        public const string InvalidOrder = "Невалидна поръчка";

        // OrderItem
        public const string InvalidOrderItem = "Невалиден продукт към поръчката";
        public const string InvalidOrderItemForUser = "Невалиден продукт към поръчката за текущия потребител";

        // Product
        public const string InvalidProduct = "Невалиден продукт";
        public const string ProductQuantityLessThan1 = "Количеството на продукта трябва да бъде по-голямо от 0";

        // User
        public const string InvalidUser = "Невалиден потребител";
        public const string AlreadyExistingUser = "Потребител с този имейл вече същестува";
        public const string NonExistentUser = "Няма потребител с този имейл";
        public const string InvalidUserPassword = "Невалидна парола";
    }
}
