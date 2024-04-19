using Microsoft.Extensions.DependencyInjection;

using FoodDeliveryWebsite.Services;
using FoodDeliveryWebsite.Models.Enums;

namespace FoodDeliveryWebsite.UnitTests.Products
{
    public class GetAvailableProductsAsyncTests : BaseServiceTests
    {
        private IProductService productService => this.ServiceProvider.GetRequiredService<IProductService>();

        public GetAvailableProductsAsyncTests()
        {
            PopulateDB().Wait();
        }

        [Fact]
        public async Task GetAvailableProducts_ReturnsAllAvailableProducts()
        {
            // Arrange
            int expectedProductsCount = 7;

            // Act
            var availableProducts = await productService.GetAvailableProductsAsync();

            // Assert
            Assert.NotNull(availableProducts);
            Assert.NotEmpty(availableProducts);
            Assert.Equal(expectedProductsCount, availableProducts.Count);
        }

        [Fact]
        public async Task GetAvailableProducts_ReturnsCorrectDataForProducts()
        {
            // Arrange

            // Act
            var availableProducts = await productService.GetAvailableProductsAsync();
            var expectedProductsCount = DbContext.Products.Where(p => p.Status == ProductStatus.Available).Count();

            // Assert
            Assert.Equal(expectedProductsCount, availableProducts.Count);
        }
    }
}
