using Microsoft.Extensions.DependencyInjection;

using FoodDeliveryWebsite.CustomExceptions;
using FoodDeliveryWebsite.Services;

namespace FoodDeliveryWebsite.UnitTests.OrderItems
{
    public class GetOrderItemsAsyncTests : BaseServiceTests
    {
        private IOrderItemService orderItemService => this.ServiceProvider.GetRequiredService<IOrderItemService>();

        public GetOrderItemsAsyncTests()
        {
            PopulateDB().Wait();
        }

        [Fact]
        public async Task GetOrderItemsAsync_ValidUser_ReturnsOrderItems()
        {
            // Arrange
            var userEmail = "ivan@gmail.com";
            var expectedOrderItemCount = 1;

            // Act
            var result = await orderItemService.GetOrderItemsAsync(userEmail);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedOrderItemCount, result.Count);
        }

        [Fact]
        public async Task GetOrderItemsAsync_InvalidUser_ShouldThrowNotFoundException()
        {
            // Arrange
            var invalidUserEmail = "invaliduser@gmail.com";

            // Act and Assert
            await Assert.ThrowsAsync<NotFoundException>(() => orderItemService.GetOrderItemsAsync(invalidUserEmail));
        }
    }
}
