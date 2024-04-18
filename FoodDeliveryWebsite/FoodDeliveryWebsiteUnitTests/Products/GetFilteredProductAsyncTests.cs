using Microsoft.Extensions.DependencyInjection;

using FoodDeliveryWebsite.Models.Enums;
using FoodDeliveryWebsite.Services;

namespace FoodDeliveryWebsite.UnitTests.Products
{
    public class GetFilteredProductAsyncTests : BaseServiceTests
    {
        private IProductService productService => this.ServiceProvider.GetRequiredService<IProductService>();

        public GetFilteredProductAsyncTests()
        {
            PopulateDB().Wait();
        }

        [Fact]
        public async Task GetFilteredProductAsync_ReturnsFilteredProducts()
        {
            // Arrange
            int expectedProductsCount = 1;

            // Act
            var result = await productService.GetFilteredProductAsync(ProductType.Dessert);

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(expectedProductsCount, result.Count());
            Assert.All(result, p => Assert.Equal(ProductType.Dessert, p.Type));
            Assert.All(result, p => Assert.Equal(ProductStatus.Available, p.Status));
        }
    }
}
