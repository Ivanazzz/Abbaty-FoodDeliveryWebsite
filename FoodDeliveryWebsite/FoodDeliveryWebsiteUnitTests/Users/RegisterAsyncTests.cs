using Microsoft.Extensions.DependencyInjection;

using FoodDeliveryWebsite.CustomExceptions;
using FoodDeliveryWebsite.Models.Dtos.UserDtos;
using FoodDeliveryWebsite.Models.Enums;
using FoodDeliveryWebsite.Services;

namespace FoodDeliveryWebsite.UnitTests.Users
{
    public class RegisterAsyncTests : BaseServiceTests
    {
        private IUserService userService => this.ServiceProvider.GetRequiredService<IUserService>();

        public RegisterAsyncTests()
        {
            PopulateDB().Wait();
        }

        [Fact]
        public async Task RegisterAsync_WithValidUser_ShouldSuccess()
        {
            // Arrange
            var userRegistrationDto = new UserRegistrationDto
            {
                FirstName = "Емил",
                LastName = "Емилов",
                Gender = Gender.Male,
                Email = "emil@abv.bg",
                Password = "strongPass!12345",
                PasswordConfirmation = "strongPass!12345",
                PhoneNumber = "+359648839368"
            };

            // Act
            await userService.RegisterAsync(userRegistrationDto);
            var addedUser = DbContext.Users.SingleOrDefault(u => u.FirstName == userRegistrationDto.FirstName);

            // Assert
            Assert.NotNull(addedUser);
        }

        [Fact]
        public async Task RegisterAsync_WithExistingUser_ThrowsBadRequestException()
        {
            // Arrange
            string existingEmail = "ivan@gmail.com";
            var userRegistrationDto = new UserRegistrationDto
            {
                FirstName = "Емил",
                LastName = "Емилов",
                Gender = Gender.Male,
                Email = existingEmail,
                Password = "strongPass!12345",
                PasswordConfirmation = "strongPass!12345",
                PhoneNumber = "+359648839368"
            };

            // Act & Assert
            await Assert.ThrowsAsync<BadRequestException>(() => userService.RegisterAsync(userRegistrationDto));
        }

        [Fact]
        public async Task RegisterAsync_WithDeletedUser_ShouldSuccess()
        {
            // Arrange
            string email = "ivan@gmail.com";
            var user = DbContext.Users.SingleOrDefault(u => u.Email == email);
            user.IsDeleted = true;
            await DbContext.SaveChangesAsync();

            var userRegistrationDto = new UserRegistrationDto
            {
                FirstName = "Емил",
                LastName = "Емилов",
                Gender = Gender.Male,
                Email = email,
                Password = "strongPass!12345",
                PasswordConfirmation = "strongPass!12345",
                PhoneNumber = "+359648839368"
            };

            // Act
            await userService.RegisterAsync(userRegistrationDto);
            var addedUser = DbContext.Users.SingleOrDefault(u => u.FirstName == userRegistrationDto.FirstName);

            // Assert
            Assert.NotNull(addedUser);
        }
    }
}
