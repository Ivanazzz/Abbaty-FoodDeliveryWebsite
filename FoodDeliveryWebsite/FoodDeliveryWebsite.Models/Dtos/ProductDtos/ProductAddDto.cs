using Microsoft.AspNetCore.Http;

using FoodDeliveryWebsite.Models.Enums;

namespace FoodDeliveryWebsite.Models.Dtos.ProductDtos
{
    public class ProductAddDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public ProductType Type { get; set; }

        public ProductStatus Status { get; set; }

        public int Grams { get; set; }

        public IFormFile? Image { get; set; }
    }
}
