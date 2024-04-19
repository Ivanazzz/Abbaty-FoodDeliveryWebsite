using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using FoodDeliveryWebsite.CustomExceptions;
using FoodDeliveryWebsite.Models.Dtos.UserDtos;
using FoodDeliveryWebsite.Models.Enums;
using FoodDeliveryWebsite.Services;

namespace FoodDeliveryWebsite.UnitTests.Users
{
    public class UpdateUserAsyncTests : BaseServiceTests
    {
        private IUserService userService => this.ServiceProvider.GetRequiredService<IUserService>();

        public UpdateUserAsyncTests()
        {
            PopulateDB().Wait();
        }

        [Fact]
        public async Task UpdateUserAsync_WithExistingEmail_UpdatesUser()
        {
            // Arrange
            var email = "ivan@gmail.com";
            var updatedUserDto = new UserDto
            {
                Id = 2,
                FirstName = "Иван",
                LastName = "Попов",
                Gender = Gender.Male,
                PhoneNumber = "+359 88 8888 888",
                Role = UserRole.Client,
            };

            // Act
            await userService.UpdateUserAsync(email, updatedUserDto);
            var updatedUser = await DbContext.Users.SingleOrDefaultAsync(u => u.Email == email);

            // Assert
            Assert.NotNull(updatedUser);
            Assert.Equal(updatedUserDto.FirstName, updatedUser.FirstName);
            Assert.Equal(updatedUserDto.LastName, updatedUser.LastName);
        }

        [Fact]
        public async Task UpdateUserAsync_WithNonExistingEmail_ThrowsNotFoundException()
        {
            // Arrange
            var email = "invalid@gmail.com";
            var updatedUserDto = new UserDto
            {
                Id = 2,
                FirstName = "Иван",
                LastName = "Попов",
                Gender = Gender.Male,
                PhoneNumber = "+359 88 8888 888",
                Role = UserRole.Client,
            };

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => userService.UpdateUserAsync(email, updatedUserDto));
        }

        [Fact]
        public async Task UpdateUserAsync_WithDeletedUser_ThrowsNotFoundException()
        {
            // Arrange
            var email = "vladi@abv.bg";
            var updatedUserDto = new UserDto
            {
                Id = 4,
                FirstName = "Владимир",
                LastName = "Стефанов",
                Gender = Gender.Male,
                PhoneNumber = "+359 22 2222 222",
                Role = UserRole.Client,
            };

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => userService.UpdateUserAsync(email, updatedUserDto));
        }

        [Fact]
        public async Task UpdateUserAsync_WithInvalidUserDto_ThrowsBadRequestException()
        {
            // Arrange
            var email = "ivan@gmail.com";
            var updatedUserDto = new UserDto
            {
                Id = 2,
                FirstName = "Invalid",
                LastName = "Попов",
                Gender = Gender.Male,
                PhoneNumber = "+359 88 8888 888",
                Role = UserRole.Client,
            };

            // Act & Assert
            await Assert.ThrowsAsync<BadRequestException>(() => userService.UpdateUserAsync(email, updatedUserDto));
        }
    }
}
