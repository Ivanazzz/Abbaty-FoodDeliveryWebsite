using FoodDeliveryWebsite.CustomExceptions;
using FoodDeliveryWebsite.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDeliveryWebsite.UnitTests.Orders
{
    public class GetOrdersAsyncTests : BaseServiceTests
    {
        private IOrderService orderService => this.ServiceProvider.GetRequiredService<IOrderService>();

        public GetOrdersAsyncTests()
        {
            PopulateDB().Wait();
        }

        [Fact]
        public async Task GetOrdersAsync_WithValidUser_ReturnsOrders()
        {
            // Arrange
            var userEmail = "ivan@gmail.com";
            int currentPage = 1;
            int pageSize = 100;
            int expectedOrdersCount = 3;

            // Act
            var orders = await orderService.GetOrdersAsync(userEmail, currentPage, pageSize);

            // Assert
            Assert.Equal(expectedOrdersCount, orders.TotalCount);
        }

        [Fact]
        public async Task GetOrdersAsync_WithInvalidUser_ThrowsNotFoundException()
        {
            // Arrange
            var invalidUserEmail = "nonexistentuser@gmail.com";

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => orderService.GetOrdersAsync(invalidUserEmail, 1, 100));
        }
    }
}
