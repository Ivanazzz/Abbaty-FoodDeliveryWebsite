using Microsoft.Extensions.DependencyInjection;

using FoodDeliveryWebsite.CustomExceptions;
using FoodDeliveryWebsite.Services;

namespace FoodDeliveryWebsite.UnitTests.OrderItems
{
    public class UpdateOrderItemAsyncTests : BaseServiceTests
    {
        private IOrderItemService orderItemService => this.ServiceProvider.GetRequiredService<IOrderItemService>();

        public UpdateOrderItemAsyncTests()
        {
            PopulateDB().Wait();
        }

        [Fact]
        public async Task UpdateOrderItemAsync_WithValidUserAndOrderItem_ShouldUpdateOrderItemQuantity()
        {
            // Arrange
            string userEmail = "ivan@gmail.com";
            int orderItemId = 7;
            int newQuantity = 4;

            // Act
            var updatedOrderItemDto = await orderItemService.UpdateOrderItemAsync(userEmail, orderItemId, newQuantity);

            // Assert
            Assert.NotNull(updatedOrderItemDto);
            Assert.Equal(newQuantity, updatedOrderItemDto.ProductQuantity);
        }

        [Fact]
        public async Task UpdateOrderItemAsync_WithInvalidUser_ShouldThrowNotFoundException()
        {
            // Arrange
            string userEmail = "nonexistentuser@gmail.com";
            int orderItemId = 7;
            int newQuantity = 4;

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => orderItemService.UpdateOrderItemAsync(userEmail, orderItemId, newQuantity));
        }

        [Fact]
        public async Task UpdateOrderItemAsync_InvalidOrderItem_ShouldThrowNotFoundException()
        {
            // Arrange
            string userEmail = "ivan@gmail.com";
            int orderItemId = 999;
            int newQuantity = 4;

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => orderItemService.UpdateOrderItemAsync(userEmail, orderItemId, newQuantity));
        }

        [Fact]
        public async Task UpdateOrderItemAsync_OrderItemWithOrderIdNotNull_ShouldThrowBadRequestException()
        {
            // Arrange
            var userEmail = "ivan@gmail.com";
            var orderItemId = 2;
            var newQuantity = 3;

            // Act & Assert
            await Assert.ThrowsAsync<BadRequestException>(() => orderItemService.UpdateOrderItemAsync(userEmail, orderItemId, newQuantity));
        }

        [Fact]
        public async Task UpdateOrderItemAsync_OrderItemNotBelongingToUser_ShouldThrowNotFoundException()
        {
            // Arrange
            string userEmail = "maria@abv.bg";
            int orderItemId = 7;
            int newQuantity = 4;

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => orderItemService.UpdateOrderItemAsync(userEmail, orderItemId, newQuantity));
        }
    }
}
