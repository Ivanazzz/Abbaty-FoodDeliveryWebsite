using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using FoodDeliveryWebsite.Models.Common;
using FoodDeliveryWebsite.Models.Enums;

namespace FoodDeliveryWebsite.Models.Entities
{
    public class Product : IEntity, IAuditable
    {
        public int Id { get; set; }

        public DateTime CreateDate { get; set; }

        public int CreatorUserId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public ProductType Type { get; set; }

        public ProductStatus Status { get; set; }

        public int Grams { get; set; }

        public bool IsDeleted { get; set; }

        public byte[] Image { get; set; }

        public string ImageName { get; set; }

        public string ImageMimeType { get; set; }

        //public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        public class ProductConfiguration : IEntityTypeConfiguration<Product>
        {
            public void Configure(EntityTypeBuilder<Product> builder)
            {
                builder.Property(b => b.Name)
                       .IsRequired();

                builder.Property(b => b.Description)
                       .IsRequired();

                builder.Property(b => b.Image)
                       .IsRequired();
            }
        }
    }
}
