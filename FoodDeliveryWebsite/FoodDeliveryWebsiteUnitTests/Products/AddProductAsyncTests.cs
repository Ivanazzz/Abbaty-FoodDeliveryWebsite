using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

using FoodDeliveryWebsite.CustomExceptions;
using FoodDeliveryWebsite.Models.Dtos.ProductDtos;
using FoodDeliveryWebsite.Models.Enums;
using FoodDeliveryWebsite.Services;

namespace FoodDeliveryWebsite.UnitTests.Products
{
    public class AddProductAsyncTests : BaseServiceTests
    {
        private IProductService productService => this.ServiceProvider.GetRequiredService<IProductService>();

        public AddProductAsyncTests()
        {
            PopulateDB().Wait();
        }

        [Fact]
        public async Task AddProductAsync_WithValidData_ProductAddedSuccessfully()
        {
            // Arrange
            var productDto = new ProductAddDto
            {
                Name = "Тест",
                Description = "Още по-дълго описание за тестовия продукт ",
                Price = 10.99m,
                Type = ProductType.Main,
                Status = ProductStatus.Available,
                Grams = 200
            };

            // Act
            await productService.AddProductAsync(productDto);
            var addedProduct = await DbContext.Products.SingleOrDefaultAsync(p => p.Name == productDto.Name);

            // Assert
            Assert.NotNull(addedProduct);
            Assert.Null(addedProduct.Image);
        }

        [Fact]
        public async Task AddProductAsync_InvalidData_ThrowsBadRequestException()
        {
            // Arrange
            var productDto = new ProductAddDto
            {
                // Missing required fields to trigger validation error
            };

            // Act & Assert
            await Assert.ThrowsAsync<BadRequestException>(() => productService.AddProductAsync(productDto));
        }
    }
}
