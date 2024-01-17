using Microsoft.EntityFrameworkCore;

using FluentValidation;

using FoodDeliveryWebsite.Models;
using FoodDeliveryWebsite.Models.Dtos;
using FoodDeliveryWebsite.Models.Entities;
using FoodDeliveryWebsite.Models.Enums;
using FoodDeliveryWebsite.Models.Validations;

namespace FoodDeliveryWebsite.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly FoodDeliveryWebsiteDbContext context;

        public ProductRepository(FoodDeliveryWebsiteDbContext context)
        {
            this.context = context;
        }

        public async Task<ProductDto[]> GetProductsAsync()
        {
            var currentProducts = await context.Product.Where(p => p.Status == ProductStatus.Available).ToListAsync();

            List<ProductDto> products = new List<ProductDto>();
            foreach (var product in currentProducts)
            {
                products.Add(new ProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Type = product.Type,
                    Status = product.Status,
                    Grams = product.Grams,
                    //Image = product.Image
                });
            }

            return products.ToArray();
        }

        public async Task AddProductAsync(ProductDto productDto)
        {
            Product product = new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                Type = productDto.Type,
                Status = productDto.Status,
                Grams = productDto.Grams,
                //Image = productDto.Image
            };

            ProductValidator validator = new ProductValidator();
            validator.ValidateAndThrow(product);

            context.Product.Add(product);
            await context.SaveChangesAsync();
        }

        public Task UpdateProductAsync(ProductDto productDto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProductAsync(int id)
        {
            // Staus = ProductStatus.Unavailable;
            throw new NotImplementedException();
        }
    }
}
