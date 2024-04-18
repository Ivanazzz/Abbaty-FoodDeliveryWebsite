using Microsoft.Extensions.DependencyInjection;

using FoodDeliveryWebsite.Services;

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

            // Assert
            foreach (var product in availableProducts)
            {
                Assert.NotNull(product.Name);
                Assert.NotNull(product.Description);
                Assert.NotNull(product.Type);
                Assert.NotNull(product.Status);
                Assert.NotNull(product.Grams);
                Assert.NotNull(product.Price);
                Assert.NotNull(product.Image);
                Assert.NotNull(product.ImageName);
                Assert.NotNull(product.ImageMimeType);
            }
        }
    }
}
