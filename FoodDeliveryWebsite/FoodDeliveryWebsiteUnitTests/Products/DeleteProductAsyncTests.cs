using Microsoft.Extensions.DependencyInjection;

using FoodDeliveryWebsite.Services;
using FoodDeliveryWebsite.CustomExceptions;
using FoodDeliveryWebsite.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryWebsite.UnitTests.Products
{
    public class DeleteProductAsyncTests : BaseServiceTests
    {
        private IProductService productService => this.ServiceProvider.GetRequiredService<IProductService>();

        public DeleteProductAsyncTests()
        {
            PopulateDB().Wait();
        }

        [Fact]
        public async Task DeleteProductAsync_WithValidProductId_DeletesProduct()
        {
            // Arrange
            int productIdToDelete = 1;

            // Act
            await productService.DeleteProductAsync(productIdToDelete);
            var result = await DbContext.Products.SingleOrDefaultAsync(p => p.Id == productIdToDelete);

            // Assert
            Assert.Equal(ProductStatus.Unavailable, result.Status);
            Assert.Equal(true, result.IsDeleted);
        }

        [Fact]
        public async Task DeleteProductAsync_WithInvalidProductId_ThrowsNotFoundException()
        {
            // Arrange
            int invalidProductId = 999;

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => productService.DeleteProductAsync(invalidProductId));
        }
    }
}
