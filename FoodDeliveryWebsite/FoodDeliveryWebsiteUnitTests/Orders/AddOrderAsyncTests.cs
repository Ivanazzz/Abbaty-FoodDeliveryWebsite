using Microsoft.Extensions.DependencyInjection;

using FoodDeliveryWebsite.Services;
using FoodDeliveryWebsite.CustomExceptions;
using FoodDeliveryWebsite.Models.Dtos.OrderDtos;
using FoodDeliveryWebsite.Models.Entities;

namespace FoodDeliveryWebsite.UnitTests.Orders
{
    public class AddOrderAsyncTests : BaseServiceTests
    {
        private IOrderService orderService => this.ServiceProvider.GetRequiredService<IOrderService>();

        public AddOrderAsyncTests()
        {
            PopulateDB().Wait();
        }

        [Fact]
        public async Task AddOrderAsync_ValidOrder_ShouldAddOrder()
        {
            // Arrange
            var orderDto = new OrderDto
            {
                UserId = 2,
                DiscountId = null,
                AddressId = 1,
                OrderItems = new List<OrderItem>()
                {
                    new OrderItem
                    {
                        Id = 8,
                        CreateDate = new DateTime(2024, 4, 3, 17, 22, 17, DateTimeKind.Utc),
                        CreatorUserId = 2,
                        ProductQuantity = 1,
                        Price = 16.49m * 1,
                        UserId = 2,
                        OrderId = null,
                        ProductId = 4
                    }
                },
                TotalPrice = 16.49m,
                DeliveryPrice = 7
            };

            int expectedOrdersCount = 4;
            string userEmail = "ivan@gmail.com";

            // Act
            await orderService.AddOrderAsync(userEmail, orderDto);
            var orders = await orderService.GetOrdersAsync("admin@gmail.com", 1, 100);

            // Assert
            Assert.Equal(expectedOrdersCount, orders.TotalCount);
        }

        [Fact]
        public async Task AddOrderAsync_WithNullOrderDto_ShouldThrowNotFoundException()
        {
            // Arrange
            OrderDto orderDto = null;
            var userEmail = "ivan@gmail.com";

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => orderService.AddOrderAsync(userEmail, orderDto));
        }

        [Fact]
        public async Task AddOrderAsync_WithInvalidUser_ShouldThrowNotFoundException()
        {
            // Arrange
            var orderDto = new OrderDto
            {
                UserId = 2,
                DiscountId = null,
                AddressId = 1,
                OrderItems = new List<OrderItem>()
                {
                    new OrderItem
                    {
                        Id = 8,
                        CreateDate = new DateTime(2024, 4, 3, 17, 22, 17, DateTimeKind.Utc),
                        CreatorUserId = 2,
                        ProductQuantity = 1,
                        Price = 16.49m * 1,
                        UserId = 2,
                        OrderId = null,
                        ProductId = 4
                    }
                },
                TotalPrice = 16.49m,
                DeliveryPrice = 7
            };
            var userEmail = "invaliduser@example.com";

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => orderService.AddOrderAsync(userEmail, orderDto));
        }

        [Fact]
        public async Task AddOrderAsync_WithAddressIdZero_ShouldThrowNotFoundException()
        {
            // Arrange
            var orderDto = new OrderDto
            {
                UserId = 2,
                DiscountId = null,
                AddressId = 0,
                OrderItems = new List<OrderItem>()
                {
                    new OrderItem
                    {
                        Id = 8,
                        CreateDate = new DateTime(2024, 4, 3, 17, 22, 17, DateTimeKind.Utc),
                        CreatorUserId = 2,
                        ProductQuantity = 1,
                        Price = 16.49m * 1,
                        UserId = 2,
                        OrderId = null,
                        ProductId = 4
                    }
                },
                TotalPrice = 16.49m,
                DeliveryPrice = 7
            };
            var userEmail = "ivan@gmail.com";

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => orderService.AddOrderAsync(userEmail, orderDto));
        }

        [Fact]
        public async Task AddOrderAsync_WithTotalPriceZero_ShouldThrowNotFoundException()
        {
            // Arrange
            var orderDto = new OrderDto
            {
                UserId = 2,
                DiscountId = null,
                AddressId = 1,
                OrderItems = new List<OrderItem>()
                {
                    new OrderItem
                    {
                        Id = 8,
                        CreateDate = new DateTime(2024, 4, 3, 17, 22, 17, DateTimeKind.Utc),
                        CreatorUserId = 2,
                        ProductQuantity = 1,
                        Price = 16.49m * 1,
                        UserId = 2,
                        OrderId = null,
                        ProductId = 4
                    }
                },
                TotalPrice = 0,
                DeliveryPrice = 7
            };
            var userEmail = "ivan@gmail.com";

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => orderService.AddOrderAsync(userEmail, orderDto));
        }
    }
}
