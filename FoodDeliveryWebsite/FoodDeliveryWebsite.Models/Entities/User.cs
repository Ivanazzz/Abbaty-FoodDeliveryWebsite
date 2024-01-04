using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

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

        public int AddressId { get; set; }

        public Address Address { get; set; }

        public UserRole Role { get; set; }

        public ICollection<Order> Orders { get; set; }

        public class UserConfiguration : IEntityTypeConfiguration<User>
        {
            public void Configure(EntityTypeBuilder<User> builder)
            {
                builder.Property(b => b.FirstName)
                       .IsRequired();

                builder.Property(b => b.LastName)
                       .IsRequired();

                builder.Property(b => b.Email)
                       .IsRequired();

                builder.Property(b => b.Password)
                       .IsRequired();

                builder.Property(b => b.PhoneNumber)
                       .IsRequired();
            }
        }
    }
}
