using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

using AutoMapper;
using FluentValidation;

using FoodDeliveryWebsite.CustomExceptions;
using FoodDeliveryWebsite.Models;
using FoodDeliveryWebsite.Models.Entities;
using FoodDeliveryWebsite.Models.Enums;
using FoodDeliveryWebsite.Models.Validations;
using FoodDeliveryWebsite.Repositories.CustomExceptionMessages;
using FoodDeliveryWebsite.Repositories.CustomExceptions;
using FoodDeliveryWebsite.Models.Dtos.ProductDtos;

namespace FoodDeliveryWebsite.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMapper mapper;
        private readonly FoodDeliveryWebsiteDbContext context;

        public ProductRepository(FoodDeliveryWebsiteDbContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<List<ProductGetDto>> GetAvailableProductsAsync()
        {
            var products = await context.Products
                .Where(p => p.Status == ProductStatus.Available)
                .Select(p => mapper.Map<ProductGetDto>(p))
                .ToListAsync();

            return products;
        }

        public async Task<List<ProductGetDto>> GetAllProductsAsync()
        {
            var products = await context.Products
                .Select(p => mapper.Map<ProductGetDto>(p))
                .ToListAsync();

            return products;
        }

        public async Task<ProductGetDto> GetSelectedProductAsync(int id)
        {
            var product = await context.Products
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null || product.IsDeleted == true)
            {
                throw new NotFoundException(ExceptionMessages.InvalidProduct);
            }

            ProductGetDto productDto = mapper.Map<ProductGetDto>(product);

            return productDto;
        }

        public async Task<List<ProductGetDto>> GetFilteredProductAsync(ProductType productType)
        {
            var products = await context.Products
                .Where(p => p.Type == productType
                    && p.Status == ProductStatus.Available)
                .Select(p => mapper.Map<ProductGetDto>(p))
                .ToListAsync();

            return products;
        }

        public async Task<List<ProductGetDto>> GetCustomFilteredProductAsync(ProductFilterDto filter)
        {
            var products = await filter
                .WhereBuilder(context.Products.AsQueryable())
                .Select(p => mapper.Map<ProductGetDto>(p))
                .ToListAsync();

            return products;
        }

        public async Task<List<ProductGetDto>> GetProductsWithStatusAsync(ProductStatus productStatus)
        {
            var products = await context.Products
                .Where(p => p.Status == productStatus 
                    && p.IsDeleted == false)
                .Select(p => mapper.Map<ProductGetDto>(p))
                .ToListAsync();

            return products;
        }

        public async Task AddProductAsync(ProductAddDto productDto)
        {
            Product product = new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                Type = productDto.Type,
                Status = productDto.Status,
                Grams = productDto.Grams,
                IsDeleted = false,
            };

            ProductValidator validator = new ProductValidator();
            var result = validator.Validate(product);

            foreach (var failure in result.Errors)
            {
                if (failure.CustomState is BadRequestException bre)
                {
                    throw bre;
                }
            }

            if (productDto.Image == null)
            {
                throw new Exception("No selected image");
            }

            byte[] imageBytes = await ConvertIFormFileToByteArray(productDto.Image);
            product.Image = imageBytes;
            product.ImageName = productDto.Image.FileName;
            product.ImageMimeType = productDto.Image.ContentType;

            context.Products.Add(product);
            await context.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(ProductGetDto productDto)
        {
            var product = await context.Products
                .FirstOrDefaultAsync(p => p.Id == productDto.Id);

            if (product == null)
            {
                throw new NotFoundException(ExceptionMessages.InvalidProduct);
            }

            product.Name = productDto.Name;
            product.Description = productDto.Description;
            product.Price = productDto.Price;
            product.Grams = productDto.Grams;
            product.Type = productDto.Type;
            product.Status = productDto.Status;

            ProductValidator validator = new ProductValidator();
            var result = validator.Validate(product);

            foreach (var failure in result.Errors)
            {
                if (failure.CustomState is BadRequestException bre)
                {
                    throw bre;
                }
            }

            context.Products.Update(product);
            await context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await context.Products
                .FirstOrDefaultAsync(p => p.Id == id 
                    && p.IsDeleted == false);

            if (product == null)
            {
                throw new NotFoundException(ExceptionMessages.InvalidProduct);
            }

            product.Status = ProductStatus.Unavailable;
            product.IsDeleted = true;

            context.Products.Update(product);
            await context.SaveChangesAsync();
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
