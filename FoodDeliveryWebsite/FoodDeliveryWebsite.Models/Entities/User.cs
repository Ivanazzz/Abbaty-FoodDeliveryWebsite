using FoodDeliveryWebsite.Models.Common;
using FoodDeliveryWebsite.Models.Enums;


namespace FoodDeliveryWebsite.Models.Entities
{
    public class User : IEntity, IAuditable
    {
        public int Id { get; set; }

        public DateTime CreateDate { get; set; }

        public int CreatorUserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}"; 

        public Gender Gender { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string PhoneNumber { get; set; }

        public bool IsDeleted { get; set; }

        public UserRole Role { get; set; }

        public string Salt { get; set; }

        public ICollection<Address> Addresses { get; set; } = new List<Address>();

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
