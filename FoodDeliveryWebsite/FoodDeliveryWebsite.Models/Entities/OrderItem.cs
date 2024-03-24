using FoodDeliveryWebsite.Models.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryWebsite.Models.Entities
{
    public class OrderItem : IEntity, IAuditable
    {
        public int Id { get; set; }

        public DateTime CreateDate { get; set; }

        public int CreatorUserId { get; set; }

        public int ProductQuantity { get; set; }

        public decimal Price { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public int? OrderId { get; set; }

        public Order Order { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }
    }

    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.Property(b => b.Price)
                .HasPrecision(18,2);
        }
    }
}
