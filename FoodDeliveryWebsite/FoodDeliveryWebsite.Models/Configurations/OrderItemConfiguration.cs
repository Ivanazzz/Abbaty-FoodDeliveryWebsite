using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using FoodDeliveryWebsite.Models.Entities;
using FoodDeliveryWebsite.Models.SeedData;

namespace FoodDeliveryWebsite.Models.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.Property(b => b.Price)
                .HasPrecision(18, 2);

            var data = new DbSeeder();
            builder.HasData(new OrderItem[]
            {
                data.FirstOrderItem,
                data.SecondOrderItem,
                data.ThirdOrderItem,
                data.FourthOrderItem,
                data.FifthOrderItem,
                data.SixthOrderItem
            });
        }
    }
}
