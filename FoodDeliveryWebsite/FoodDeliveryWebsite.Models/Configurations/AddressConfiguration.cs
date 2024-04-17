using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using FoodDeliveryWebsite.Models.Entities;
using static FoodDeliveryWebsite.Models.Constants.AddressConstants;
using FoodDeliveryWebsite.Models.SeedData;

namespace FoodDeliveryWebsite.Models.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.Property(b => b.City)
                .IsRequired()
                .HasMaxLength(CityMaxLength);

            builder.Property(b => b.Street)
                .IsRequired()
                .HasMaxLength(StreetMaxLength);

            var data = new DbSeeder();
            builder.HasData(new Address[] 
            { 
                data.FirstAddress, 
                data.SecondAddress, 
                data.ThirdAddress 
            });
        }
    }
}
