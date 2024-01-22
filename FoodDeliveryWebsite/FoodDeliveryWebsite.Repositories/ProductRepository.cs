using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

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

        public async Task<List<ProductGetDto>> GetAvailableProductsAsync()
        {
            var currentProducts = await context.Product.Where(p => p.Status == ProductStatus.Available).ToListAsync();

            List<ProductGetDto> products = new List<ProductGetDto>();
            foreach (var product in currentProducts)
            {
                products.Add(new ProductGetDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Type = product.Type,
                    Status = product.Status,
                    Grams = product.Grams,
                    Image = product.Image,
                    ImageMimeType = product.ImageMimeType,
                    ImageName = product.ImageName
                });
            }

            return products;
        }

        public async Task<List<ProductGetDto>> GetAllProductsAsync()
        {
            var currentProducts = await context.Product.ToListAsync();

            List<ProductGetDto> products = new List<ProductGetDto>();
            foreach (var product in currentProducts)
            {
                products.Add(new ProductGetDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Type = product.Type,
                    Status = product.Status,
                    Grams = product.Grams,
                    Image = product.Image,
                    ImageMimeType = product.ImageMimeType,
                    ImageName = product.ImageName
                });
            }

            return products;
        }

        public async Task<ProductGetDto> GetSelectedProductAsync(int id)
        {
            var product = await context.Product.FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                throw new Exception("Product unavailable.");
            }

            ProductGetDto productDto = new ProductGetDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Type = product.Type,
                Status = product.Status,
                Grams = product.Grams,
                Image = product.Image,
                ImageMimeType = product.ImageMimeType,
                ImageName = product.ImageName
            };

            return productDto;
        }

        public async Task<List<ProductGetDto>> GetFilteredProductAsync(ProductType productType)
        {
            var currentProducts = await context.Product
                .Where(p => p.Type == productType 
                    && p.Status == ProductStatus.Available)
                .ToListAsync();

            List<ProductGetDto> products = new List<ProductGetDto>();
            foreach (var product in currentProducts)
            {
                products.Add(new ProductGetDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Type = product.Type,
                    Status = product.Status,
                    Grams = product.Grams,
                    Image = product.Image,
                    ImageMimeType = product.ImageMimeType,
                    ImageName = product.ImageName
                });
            }

            return products;
        }

        public async Task<List<ProductGetDto>> GetProductsWithStatusAsync(ProductStatus productStatus)
        {
            var currentProducts = await context.Product
                .Where(p => p.Status == productStatus)
                .ToListAsync();

            List<ProductGetDto> products = new List<ProductGetDto>();
            foreach (var product in currentProducts)
            {
                products.Add(new ProductGetDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Type = product.Type,
                    Status = product.Status,
                    Grams = product.Grams,
                    Image = product.Image,
                    ImageMimeType = product.ImageMimeType,
                    ImageName = product.ImageName
                });
            }

            return products;
        }

        public async Task AddProductAsync(ProductAddDto productDto)
        {
            byte[] imageBytes = await ConvertIFormFileToByteArray(productDto.Image);

            Product product = new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                Type = productDto.Type,
                Status = productDto.Status,
                Grams = productDto.Grams,
                IsDeleted = false,
                Image = imageBytes,
                ImageName = productDto.Image.FileName,
                ImageMimeType = productDto.Image.ContentType,
            };

            ProductValidator validator = new ProductValidator();
            validator.ValidateAndThrow(product);

            context.Product.Add(product);
            await context.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(ProductGetDto productDto)
        {
            var product = await context.Product.FirstOrDefaultAsync(p => p.Id == productDto.Id);

            if (product != null)
            {
                product.Name = productDto.Name;
                product.Description = productDto.Description;
                product.Price = productDto.Price;
                product.Grams = productDto.Grams;
                product.Type = productDto.Type;
                product.Status = productDto.Status;

                ProductValidator validator = new ProductValidator();
                validator.ValidateAndThrow(product);

                context.Product.Update(product);
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await context.Product.FirstOrDefaultAsync(p => p.Id == id && p.IsDeleted == false);

            if (product != null)
            {
                product.Status = ProductStatus.Unavailable;
                product.IsDeleted = true;

                context.Product.Update(product);
                await context.SaveChangesAsync();
            }
        }

        private async Task<byte[]> ConvertIFormFileToByteArray(IFormFile formFile)
        {
            using (var memoryStream = new MemoryStream())
            {
                await formFile.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
