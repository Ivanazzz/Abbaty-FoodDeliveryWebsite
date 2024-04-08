using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using FoodDeliveryWebsite.Models.Entities;
using static FoodDeliveryWebsite.Models.Constants.UserConstants;

namespace FoodDeliveryWebsite.Models.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(b => b.FirstName)
                .IsRequired()
                .HasMaxLength(FirstNameMaxLength);

            builder.Property(b => b.LastName)
                .IsRequired()
                .HasMaxLength(LastNameMaxLength);

            builder.Property(b => b.Email)
                .IsRequired();

            builder.Property(b => b.Password)
                .IsRequired();

            builder.Property(b => b.PhoneNumber)
                .IsRequired();

            builder.Property(b => b.Salt)
                .IsRequired();

            var data = new DbSeeder();
            builder.HasData(new User[] { data.Admin, data.FirstClient, data.SecondClient });
        }
    }
}
