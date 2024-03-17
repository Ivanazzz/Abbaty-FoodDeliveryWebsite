using FoodDeliveryWebsite.Models.Enums;

namespace FoodDeliveryWebsite.Models.Dtos.UserDtos
{
    public class UserDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public Gender Gender { get; set; }

        public string PhoneNumber { get; set; }

        public UserRole Role { get; set; }
    }
}
