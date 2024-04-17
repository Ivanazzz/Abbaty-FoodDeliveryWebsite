using Microsoft.Extensions.DependencyInjection;

using FoodDeliveryWebsite.Services;
using FoodDeliveryWebsite.CustomExceptions;

namespace FoodDeliveryWebsite.UnitTests.OrderItems
{
    public class DeleteOrderItemAsyncTests : BaseServiceTests
    {
        private IOrderItemService orderItemService => this.ServiceProvider.GetRequiredService<IOrderItemService>();

        public DeleteOrderItemAsyncTests()
        {
            PopulateDB().Wait();
        }

        [Fact]
        public async Task DeleteOrderItemAsync_WithValidOrderItemId_ShouldDeleteOrderItem()
        {
            // Arrange
            string userEmail = "ivan@gmail.com";
            int orderItemId = 7;
            int expectedOrderItemsCount = 0;

            // Act
            await orderItemService.DeleteOrderItemAsync(userEmail, orderItemId);

            // Assert
            var orderItems = await orderItemService.GetOrderItemsAsync(userEmail);
            Assert.Equal(expectedOrderItemsCount, orderItems.Count);
        }

        [Fact]
        public async Task DeleteOrderItemAsync_WithInvalidOrderItemId_ShouldThrowNotFoundException()
        {
            // Arrange
            string userEmail = "ivan@gmail.com";
            int invalidOrderItemId = 999;

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => orderItemService.DeleteOrderItemAsync(userEmail, invalidOrderItemId));
        }

        [Fact]
        public async Task DeleteOrderItemAsync_OrderItemWithOrderIdNotNull_ShouldThrowBadRequestException()
        {
            // Arrange
            var userEmail = "ivan@gmail.com";
            var orderItemId = 2;

            // Act & Assert
            await Assert.ThrowsAsync<BadRequestException>(() => orderItemService.DeleteOrderItemAsync(userEmail, orderItemId));
        }

        [Fact]
        public async Task DeleteOrderItemAsync_WithInvalidUser_ShouldThrowNotFoundException()
        {
            // Arrange
            string userEmail = "nonexistentuser@gmail.com";
            int orderItemId = 7;

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => orderItemService.DeleteOrderItemAsync(userEmail, orderItemId));
        }

        [Fact]
        public async Task DeleteOrderItemAsync_OrderItemNotBelongingToUser_ShouldThrowNotFoundException()
        {
            // Arrange
            string userEmail = "maria@abv.bg";
            int orderItemId = 7;

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => orderItemService.DeleteOrderItemAsync(userEmail, orderItemId));
        }
    }
}
