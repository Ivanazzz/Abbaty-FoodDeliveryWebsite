using Microsoft.Extensions.DependencyInjection;

using FoodDeliveryWebsite.Services;
using FoodDeliveryWebsite.Models.Dtos.ProductDtos;
using FoodDeliveryWebsite.CustomExceptions;

namespace FoodDeliveryWebsite.UnitTests.Products
{
    public class GetSelectedProductAsyncTests : BaseServiceTests
    {
        private IProductService productService => this.ServiceProvider.GetRequiredService<IProductService>();

        public GetSelectedProductAsyncTests()
        {
            PopulateDB().Wait();
        }

        [Fact]
        public async Task GetSelectedProductAsync_WithValidProduct_ReturnsProductDto()
        {
            // Arrange
            int productId = 1;

            // Act
            var result = await productService.GetSelectedProductAsync(productId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ProductGetDto>(result);
            Assert.Equal(productId, result.Id);
        }

        [Fact]
        public async Task GetSelectedProductAsync_WithInvalidProduct_ThrowsNotFoundException()
        {
            // Arrange
            int productId = 999;

            // Act && Assert
            await Assert.ThrowsAsync<NotFoundException>(() => productService.GetSelectedProductAsync(productId));
        }
    }
}
