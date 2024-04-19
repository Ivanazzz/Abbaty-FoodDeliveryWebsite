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
        private Image image;

        public ProductService(IRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.image = GetMissingImageInfo();
        }

        public async Task<List<ProductGetDto>> GetAvailableProductsAsync()
        {
            var products = await repository.AllReadOnly<Product>()
                .Where(p => p.Status == ProductStatus.Available)
                .ProjectTo<ProductGetDto>(mapper.ConfigurationProvider)
                .ToListAsync();

            return products;
        }

        public async Task<ProductGetDto> GetSelectedProductAsync(int id)
        {
            var product = await repository.AllReadOnly<Product>()
                .Where(p => p.Id == id
                    && !p.IsDeleted)
                .SingleOrDefaultAsync();

            if (product == null)
            {
                throw new NotFoundException(ExceptionMessages.InvalidProduct);
            }

            if (product.Image == null)
            {
                product.Image = image.Data;
                product.ImageName = image.Name;
                product.ImageMimeType = image.MimeType;
            }

            return mapper.Map<ProductGetDto>(product);
        }

        public async Task<List<ProductGetDto>> GetFilteredProductAsync(ProductType productType)
        {
            var products = await repository.AllReadOnly<Product>()
                .Where(p => p.Type == productType
                    && p.Status == ProductStatus.Available)
                .ProjectTo<ProductGetDto>(mapper.ConfigurationProvider)
                .ToListAsync();

            return products;
        }

        public async Task<List<ProductGetDto>> GetCustomFilteredProductAsync(ProductFilterDto filter)
        {
            var products = await filter
                .WhereBuilder(repository.AllReadOnly<Product>()
                    .AsQueryable())
                .ProjectTo<ProductGetDto>(mapper.ConfigurationProvider)
                .ToListAsync();

            return products;
        }

        public async Task<List<ProductGetDto>> GetProductsWithStatusAsync(ProductStatus productStatus)
        {
            var products = await repository.AllReadOnly<Product>()
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

            if (productDto.Image != null)
            {
                byte[] imageBytes = await ConvertIFormFileToByteArray(productDto.Image);
                product.Image = imageBytes;
                product.ImageName = productDto.Image.FileName;
                product.ImageMimeType = productDto.Image.ContentType;
            }

            await repository.AddAsync(product);
            await repository.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(ProductGetDto productDto)
        {
            var product = await repository.All<Product>()
                .Where(p => p.Id == productDto.Id
                    && !p.IsDeleted)
                .SingleOrDefaultAsync();

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
                .SingleOrDefaultAsync(p => p.Id == id 
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

        private Image GetMissingImageInfo()
        {
            string rootDirectory = Directory
                .GetParent(AppDomain.CurrentDomain.BaseDirectory)
                .Parent
                .Parent
                .Parent
                .Parent
                .Parent
                .FullName;
            string imagePath = Path.Combine(rootDirectory, @"FoodDeliveryWebsite\FoodDeliveryWebsite\wwwroot\ProductImages\missing.png");

            var image = new Image
            {
                Data = GetImageBytes(imagePath),
                Name = Path.GetFileName(imagePath),
                MimeType = GetImageMimeType(imagePath)
            };

            return image;
        }

        private byte[] GetImageBytes(string image)
        {
            using (FileStream fs = new FileStream(image, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    return br.ReadBytes((int)fs.Length);
                }
            }
        }

        private string GetImageMimeType(string imagePath)
        {
            // Get the file extension
            string extension = Path.GetExtension(imagePath);

            // Map common file extensions to MIME types
            switch (extension.ToLower())
            {
                case ".jpg":
                case ".jpeg":
                    return "image/jpeg";
                case ".png":
                    return "image/png";
                case ".gif":
                    return "image/gif";
                case ".bmp":
                    return "image/bmp";
                default:
                    return "application/octet-stream"; // Default MIME type for unknown extensions
            }
        }
    }
}
