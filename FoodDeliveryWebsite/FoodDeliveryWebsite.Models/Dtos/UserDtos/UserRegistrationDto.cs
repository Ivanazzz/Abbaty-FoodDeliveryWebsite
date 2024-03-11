using FoodDeliveryWebsite.Models.Entities;
using FoodDeliveryWebsite.Models.Enums;
namespace FoodDeliveryWebsite.Models.Dtos.UserDtos
{
    public class UserRegistrationDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Gender Gender { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string PasswordConfirmation { get; set; }

        public string PhoneNumber { get; set; }

        public List<Address> Addresses { get; set; }
    }
}
