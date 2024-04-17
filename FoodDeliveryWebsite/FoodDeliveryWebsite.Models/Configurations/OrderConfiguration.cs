using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using FoodDeliveryWebsite.Models.Entities;
using FoodDeliveryWebsite.Models.SeedData;

namespace FoodDeliveryWebsite.Models.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(b => b.TotalPrice)
                .HasPrecision(18, 2);

            builder.Property(b => b.DeliveryPrice)
                .HasPrecision(18, 2);

            var data = new DbSeeder();
            builder.HasData(new Order[]
            {
                data.FirstOrder,
                data.SecondOrder,
                data.ThirdOrder
            });
        }
    }
}
