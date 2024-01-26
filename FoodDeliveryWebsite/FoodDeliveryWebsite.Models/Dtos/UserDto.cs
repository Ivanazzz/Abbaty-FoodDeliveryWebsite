using FoodDeliveryWebsite.Models.Enums;

namespace FoodDeliveryWebsite.Models.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public Gender Gender { get; set; }

        public string PhoneNumber { get; set; }

        //public ICollection<Address> Addresses { get; set; }

        public UserRole Role { get; set; }

        //public ICollection<Order> Orders { get; set; }
    }
}
