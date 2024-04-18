using Microsoft.Extensions.DependencyInjection;

using FoodDeliveryWebsite.Services;
using FoodDeliveryWebsite.Models.Enums;

namespace FoodDeliveryWebsite.UnitTests.Products
{
    public class GetProductsWithStatusAsyncTests : BaseServiceTests
    {
        private IProductService productService => this.ServiceProvider.GetRequiredService<IProductService>();

        public GetProductsWithStatusAsyncTests()
        {
            PopulateDB().Wait();
        }

        [Fact]
        public async Task GetProductsWithStatusAsync_ShouldReturnAvailableProducts()
        {
            // Arrange
            var expectedCount = 7;

            // Act
            var result = await productService.GetProductsWithStatusAsync(ProductStatus.Available);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedCount, result.Count);
            Assert.All(result, p => Assert.Equal(ProductStatus.Available, p.Status));
        }

        [Fact]
        public async Task GetProductsWithStatusAsync_ShouldReturnUnavailableProducts()
        {
            // Arrange
            var expectedCount = 1;

            // Act
            var result = await productService.GetProductsWithStatusAsync(ProductStatus.Unavailable);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedCount, result.Count);
            Assert.All(result, p => Assert.Equal(ProductStatus.Unavailable, p.Status));
        }
    }
}
