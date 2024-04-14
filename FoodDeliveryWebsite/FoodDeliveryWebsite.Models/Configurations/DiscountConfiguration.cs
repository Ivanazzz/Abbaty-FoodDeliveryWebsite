using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using FoodDeliveryWebsite.Models.Entities;

namespace FoodDeliveryWebsite.Models.Configurations
{
    public class DiscountConfiguration : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            builder.Property(b => b.Code)
                .IsRequired();

            var data = new DbSeeder();
            builder.HasData(new Discount[] 
            { 
                data.FirstDiscount,
                data.SecondDiscount, 
                data.ThirdDiscount 
            });
        }
    }
}
