using Microsoft.Extensions.DependencyInjection;

using FoodDeliveryWebsite.Services;
using FoodDeliveryWebsite.Models.Dtos.ProductDtos;
using FoodDeliveryWebsite.Models.Enums;

namespace FoodDeliveryWebsite.UnitTests.Products
{
    public class GetCustomFilteredProductAsyncTests : BaseServiceTests
    {
        private IProductService productService => this.ServiceProvider.GetRequiredService<IProductService>();

        public GetCustomFilteredProductAsyncTests()
        {
            PopulateDB().Wait();
        }

        [Fact]
        public async Task GetCustomFilteredProductAsync_ShouldReturnFilteredProducts()
        {
            // Arrange
            var filterDto = new ProductFilterDto { PriceMin = 10, PriceMax = 20, Type = null };
            int expectedProductsCount = 3;

            // Act
            var result = await productService.GetCustomFilteredProductAsync(filterDto);

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(expectedProductsCount, result.Count());
            Assert.All(result, p => Assert.Equal(ProductStatus.Available, p.Status));
            Assert.All(result, p => Assert.InRange(p.Price, filterDto.PriceMin, filterDto.PriceMax));
        }

        [Fact]
        public async Task GetCustomFilteredProductAsync_WithEmptyDto_ShouldReturnAllProducts()
        {
            // Arrange
            var filterDto = new ProductFilterDto();
            int expectedProductsCount = 7;

            // Act
            var result = await productService.GetCustomFilteredProductAsync(filterDto);

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(expectedProductsCount, result.Count());
        }

        [Fact]
        public async Task GetCustomFilteredProductAsync_WithHighMinPrice_ShouldReturnEmptyList()
        {
            // Arrange
            var filterDto = new ProductFilterDto { PriceMin = 100 };
            int expectedProductsCount = 0;

            // Act
            var result = await productService.GetCustomFilteredProductAsync(filterDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedProductsCount, result.Count());
        }
    }
}
