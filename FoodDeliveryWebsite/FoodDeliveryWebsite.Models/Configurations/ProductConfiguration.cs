using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using FoodDeliveryWebsite.Models.Entities;
using static FoodDeliveryWebsite.Models.Constants.ProductConstants;
using FoodDeliveryWebsite.Models.SeedData;

namespace FoodDeliveryWebsite.Models.Configurations
{
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
                .HasPrecision(18, 2);

            builder.Property(b => b.ImageName)
                .HasMaxLength(ImageNameMaxLength);

            builder.Property(b => b.ImageMimeType)
                .HasMaxLength(ImageMimeTypeMaxLength);

            var data = new DbSeeder();
            builder.HasData(new Product[] 
            { 
                data.FirstProduct,
                data.SecondProduct,
                data.ThirdProduct,
                data.FourthProduct,
                data.FifthProduct,
                data.SixthProduct,
                data.SeventhProduct
            });
        }
    }
}
