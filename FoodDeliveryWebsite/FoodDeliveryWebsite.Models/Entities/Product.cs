using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using FoodDeliveryWebsite.Models.Common;
using FoodDeliveryWebsite.Models.Enums;
using static FoodDeliveryWebsite.Models.Constants.ProductConstants;

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
    }

    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(NameMaxLength);

            builder.Property(b => b.Description)
                .IsRequired()
                .HasMaxLength(DescriptionMaxLength);

            builder.Property(b => b.Price)
                .HasPrecision(18,2);

            builder.Property(b => b.Image)
                .IsRequired()
                .HasMaxLength(DescriptionMaxLength);

            builder.Property(b => b.ImageName)
                .IsRequired()
                .HasMaxLength(ImageNameMaxLength);

            builder.Property(b => b.ImageMimeType)
                .IsRequired()
                .HasMaxLength(ImageMimeTypeMaxLength);
        }
    }
}
