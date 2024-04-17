using Microsoft.Extensions.DependencyInjection;

using FoodDeliveryWebsite.Services;
using FoodDeliveryWebsite.CustomExceptions;

namespace FoodDeliveryWebsite.UnitTests.OrderItems
{
    public class AddOrderItemAsyncTests : BaseServiceTests
    {
        private IOrderItemService orderItemService => this.ServiceProvider.GetRequiredService<IOrderItemService>();

        public AddOrderItemAsyncTests()
        {
            PopulateDB().Wait();
        }

        [Fact]
        public async Task AddOrderItemAsync_WithValidUserAndProduct_ShouldAddOrderItem()
        {
            // Arrange
            var userEmail = "ivan@gmail.com";
            var productId = 1;
            var quantity = 1;
            var expectedOrderItemsCount = 2;

            // Act
            await orderItemService.AddOrderItemAsync(userEmail, productId, quantity);

            // Assert
            var userOrderItemsWithoutOrder = await orderItemService.GetOrderItemsAsync(userEmail);

            Assert.NotNull(userOrderItemsWithoutOrder);
            Assert.Equal(expectedOrderItemsCount, userOrderItemsWithoutOrder.Count);
        }

        [Fact]
        public async Task AddOrderItemAsync_WithInvalidUser_ShouldThrowNotFoundException()
        {
            // Arrange
            var invalidUserEmail = "invaliduser@gmail.com";
            var productId = 1;
            var quantity = 1;

            // Act and Assert
            await Assert.ThrowsAsync<NotFoundException>(() => orderItemService.AddOrderItemAsync(invalidUserEmail, productId, quantity));
        }

        [Fact]
        public async Task AddOrderItemAsync_WithInvalidProduct_ShouldThrowNotFoundException()
        {
            // Arrange
            var userEmail = "ivan@gmail.com";
            var invalidProductId = 999;
            var quantity = 1;

            // Act and Assert
            await Assert.ThrowsAsync<NotFoundException>(() => orderItemService.AddOrderItemAsync(userEmail, invalidProductId, quantity));
        }

        [Fact]
        public async Task AddOrderItemAsync_WithInvalidQuantity_ShouldThrowBadRequestException()
        {
            // Arrange
            var userEmail = "ivan@gmail.com";
            var productId = 5;
            var invalidQuantity = 0;

            // Act and Assert
            await Assert.ThrowsAsync<BadRequestException>(() => orderItemService.AddOrderItemAsync(userEmail, productId, invalidQuantity));
        }

        [Fact]
        public async Task AddOrderItemAsync_WithExistingOrderItem_ShouldIncreaseQuantity()
        {
            // Arrange
            var userEmail = "ivan@gmail.com";
            var productId = 1;
            var quantity = 1;
            var expectedOrderItemsCount = 2;

            // Act
            await orderItemService.AddOrderItemAsync(userEmail, productId, quantity);
            await orderItemService.AddOrderItemAsync(userEmail, productId, quantity);

            // Assert
            var userOrderItemsWithoutOrder = await orderItemService.GetOrderItemsAsync(userEmail);
            var addedOrderItem = userOrderItemsWithoutOrder.FirstOrDefault(oi => oi.Product.Id == productId);

            Assert.NotNull(userOrderItemsWithoutOrder);
            Assert.NotNull(addedOrderItem);
            Assert.Equal(expectedOrderItemsCount, userOrderItemsWithoutOrder.Count);
            Assert.Equal(quantity + quantity, addedOrderItem.ProductQuantity);
        }
    }
}
