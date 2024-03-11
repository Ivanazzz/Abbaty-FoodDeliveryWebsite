using FoodDeliveryWebsite.Models.Enums;
using Microsoft.AspNetCore.Http;

namespace FoodDeliveryWebsite.Models.Dtos.ProductDtos
{
    public class ProductAddDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public ProductType Type { get; set; }

        public ProductStatus Status { get; set; }

        public int Grams { get; set; }

        public IFormFile Image { get; set; }
    }
}
