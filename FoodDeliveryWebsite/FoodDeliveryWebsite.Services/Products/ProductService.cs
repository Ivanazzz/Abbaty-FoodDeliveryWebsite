using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

using FoodDeliveryWebsite.CustomExceptionMessages;
using FoodDeliveryWebsite.CustomExceptions;
using FoodDeliveryWebsite.Models.Common;
using FoodDeliveryWebsite.Models.Dtos.ProductDtos;
using FoodDeliveryWebsite.Models.Entities;
using FoodDeliveryWebsite.Models.Enums;
using FoodDeliveryWebsite.Models.Validations;

namespace FoodDeliveryWebsite.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository repository;
        private readonly IMapper mapper;

        public ProductService(IRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<List<ProductGetDto>> GetAvailableProductsAsync()
        {
            var products = await repository.All<Product>()
                .Where(p => p.Status == ProductStatus.Available)
                .ProjectTo<ProductGetDto>(mapper.ConfigurationProvider)
                .ToListAsync();

            return products;
        }

        public async Task<ProductGetDto> GetProductByIdAsync(int id)
        {
            var product = await repository.GetByIdAsync<Product>(id);

            if (product == null || product.IsDeleted)
            {
                throw new NotFoundException(ExceptionMessages.InvalidProduct);
            }

            return mapper.Map<ProductGetDto>(product);
        }

        public async Task<ProductGetDto> GetSelectedProductAsync(int id)
        {
            var product = await repository
                .GetByIdAsync<Product>(id);

            if (product == null || product.IsDeleted)
            {
                throw new NotFoundException(ExceptionMessages.InvalidProduct);
            }

            return mapper.Map<ProductGetDto>(product);
        }

        public async Task<List<ProductGetDto>> GetFilteredProductAsync(ProductType productType)
        {
            var products = await repository.All<Product>()
                .Where(p => p.Type == productType
                    && p.Status == ProductStatus.Available)
                .ProjectTo<ProductGetDto>(mapper.ConfigurationProvider)
                .ToListAsync();

            return products;
        }

        public async Task<List<ProductGetDto>> GetCustomFilteredProductAsync(ProductFilterDto filter)
        {
            var products = await filter
                .WhereBuilder(repository.All<Product>()
                    .AsQueryable())
                .ProjectTo<ProductGetDto>(mapper.ConfigurationProvider)
                .ToListAsync();

            return products;
        }

        public async Task<List<ProductGetDto>> GetProductsWithStatusAsync(ProductStatus productStatus)
        {
            var products = await repository.All<Product>()
                .Where(p => p.Status == productStatus 
                    && !p.IsDeleted)
                .ProjectTo<ProductGetDto>(mapper.ConfigurationProvider)
                .ToListAsync();

            return products;
        }

        public async Task AddProductAsync(ProductAddDto productDto)
        {
            Product product = mapper.Map<Product>(productDto);

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
                throw new BadRequestException(ExceptionMessages.NoSelectedImage);
            }

            byte[] imageBytes = await ConvertIFormFileToByteArray(productDto.Image);
            product.Image = imageBytes;
            product.ImageName = productDto.Image.FileName;
            product.ImageMimeType = productDto.Image.ContentType;

            await repository.AddAsync(product);
            await repository.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(ProductGetDto productDto)
        {
            var product = await repository.GetByIdAsync<Product>(productDto.Id);

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

            await repository.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await repository.All<Product>()
                .FirstOrDefaultAsync(p => p.Id == id 
                    && !p.IsDeleted);

            if (product == null)
            {
                throw new NotFoundException(ExceptionMessages.InvalidProduct);
            }

            product.Status = ProductStatus.Unavailable;
            product.IsDeleted = true;

            await repository.SaveChangesAsync();
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
