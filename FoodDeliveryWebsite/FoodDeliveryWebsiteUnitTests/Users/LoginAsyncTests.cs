using Microsoft.Extensions.DependencyInjection;

using FoodDeliveryWebsite.Services;
using FoodDeliveryWebsite.CustomExceptions;
using FoodDeliveryWebsite.Models.Dtos.UserDtos;

namespace FoodDeliveryWebsite.UnitTests.Users
{
    public class LoginAsyncTests : BaseServiceTests
    {
        private IUserService userService => this.ServiceProvider.GetRequiredService<IUserService>();

        public LoginAsyncTests()
        {
            PopulateDB().Wait();
        }

        [Fact]
        public async Task LoginAsync_WithValidCredentials_ReturnsUser()
        {
            // Arrange
            var userLoginDto = new UserLoginDto
            {
                Email = "ivan@gmail.com",
                Password = "parolaB!123"
            };

            // Act
            var user = await userService.LoginAsync(userLoginDto);

            // Assert
            Assert.NotNull(user);
            Assert.Equal("Иван", user.FirstName);
        }

        [Fact]
        public async Task LoginAsync_WithInvalidCredentials_ThrowsNotFoundException()
        {
            // Arrange
            var userLoginDto = new UserLoginDto
            {
                Email = "invalidemail@gmail.com",
                Password = "invalidpassword"
            };

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => userService.LoginAsync(userLoginDto));
        }

        [Fact]
        public async Task LoginAsync_DeletedUser_ThrowsNotFoundException()
        {
            // Arrange
            var userLoginDto = new UserLoginDto
            {
                Email = "vladi@abv.bg",
                Password = "randomPassword.1234"
            };

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => userService.LoginAsync(userLoginDto));
        }
        [Fact]
        public async Task LoginAsync_WithInvalidPassword_ThrowsNotFoundException()
        {
            // Arrange
            var userLoginDto = new UserLoginDto
            {
                Email = "ivan@gmail.com",
                Password = "invalidPassword"
            };

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => userService.LoginAsync(userLoginDto));
        }
    }
}
