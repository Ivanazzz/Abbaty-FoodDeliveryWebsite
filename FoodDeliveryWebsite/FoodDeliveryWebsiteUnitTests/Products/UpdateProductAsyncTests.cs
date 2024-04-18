using Microsoft.Extensions.DependencyInjection;

using FoodDeliveryWebsite.CustomExceptions;
using FoodDeliveryWebsite.Services;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryWebsite.UnitTests.Products
{
    public class UpdateProductAsyncTests : BaseServiceTests
    {
        private IProductService productService => this.ServiceProvider.GetRequiredService<IProductService>();

        public UpdateProductAsyncTests()
        {
            PopulateDB().Wait();
        }

        [Fact]
        public async Task UpdateProductAsync_WithExistingProduct_ShouldUpdateSuccessfully()
        {
            // Arrange
            var existingProduct = await productService.GetSelectedProductAsync(1);
            int gramsToUpdate = 500;

            // Act
            existingProduct.Grams = gramsToUpdate;
            await productService.UpdateProductAsync(existingProduct);
            var updatedProduct = await DbContext.Products.SingleOrDefaultAsync(p => p.Id == existingProduct.Id);

            // Assert
            Assert.NotNull(updatedProduct);
            Assert.Equal(gramsToUpdate, updatedProduct.Grams);
        }

        [Fact]
        public async Task UpdateProductAsync_NonExistingProduct_ShouldThrowNotFoundException()
        {
            // Arrange
            var existingProduct = await productService.GetSelectedProductAsync(1);
            var nonExistingProductId = 999;

            // Act
            existingProduct.Id = nonExistingProductId;

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(() => productService.UpdateProductAsync(existingProduct));
        }

        [Fact]
        public async Task UpdateProductAsync_InvalidProduct_ShouldThrowValidationException()
        {
            // Arrange
            var existingProduct = await productService.GetSelectedProductAsync(1);

            // Act
            existingProduct.Name = "";

            // Assert
            await Assert.ThrowsAsync<BadRequestException>(() => productService.UpdateProductAsync(existingProduct));
        }
    }
}
