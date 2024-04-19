using Microsoft.Extensions.DependencyInjection;

using FoodDeliveryWebsite.Services;
using FoodDeliveryWebsite.Models.Enums;

namespace FoodDeliveryWebsite.UnitTests.Users
{
    public class GetUserAsyncTests : BaseServiceTests
    {
        private IUserService userService => this.ServiceProvider.GetRequiredService<IUserService>();

        public GetUserAsyncTests()
        {
            PopulateDB().Wait();
        }

        [Fact]
        public async Task GetUserAsync_WithValidEmail_ReturnsUserDto()
        {
            // Arrange
            string email = "ivan@gmail.com";
            int expectedId = 2;
            UserRole expectedRole = UserRole.Client;

            // Act
            var userDto = await userService.GetUserAsync(email);

            // Assert
            Assert.NotNull(userDto);
            Assert.Equal(expectedId, userDto.Id);
            Assert.Equal(expectedRole, userDto.Role);
        }

        [Fact]
        public async Task GetUserAsync_WithInvalidEmail_ReturnsNull()
        {
            // Arrange
            string email = "invalid@gmail.com";

            // Act
            var userDto = await userService.GetUserAsync(email);

            // Assert
            Assert.Null(userDto);
        }

        [Fact]
        public async Task GetUserAsync_WithDeletedUser_ReturnsNull()
        {
            // Arrange
            string email = "vladi@abv.bg";

            // Act
            var userDto = await userService.GetUserAsync(email);

            // Assert
            Assert.Null(userDto);
        }
    }
}
