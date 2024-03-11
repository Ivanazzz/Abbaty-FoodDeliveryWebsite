using FoodDeliveryWebsite.Models.Enums;

namespace FoodDeliveryWebsite.Models.Dtos.ProductDtos
{
    public class ProductGetDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public ProductType Type { get; set; }

        public ProductStatus Status { get; set; }

        public int Grams { get; set; }

        public byte[] Image { get; set; }

        public string ImageName { get; set; }

        public string ImageMimeType { get; set; }
    }
}
