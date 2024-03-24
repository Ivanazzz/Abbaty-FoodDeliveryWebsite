using FoodDeliveryWebsite.Models.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryWebsite.Models.Entities
{
    public class Order : IEntity, IAuditable
    {
        public int Id { get; set; }

        public DateTime CreateDate { get; set; }

        public int CreatorUserId { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public int? DiscountId { get; set; }

        public Discount Discount { get; set; }

        public int AddressId { get; set; }

        public Address Address { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        public decimal TotalPrice { get; set; }

        public decimal DeliveryPrice { get; set; } 
    }

    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(b => b.TotalPrice)
                .HasPrecision(18, 2);

            builder.Property(b => b.DeliveryPrice)
                .HasPrecision(18, 2);
        }
    }
}
