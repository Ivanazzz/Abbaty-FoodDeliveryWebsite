using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

using FoodDeliveryWebsite.Services;
using FoodDeliveryWebsite.CustomExceptions;

namespace FoodDeliveryWebsite.UnitTests.Users
{
    public class DeleteUserAsyncTests : BaseServiceTests
    {
        private IUserService userService => this.ServiceProvider.GetRequiredService<IUserService>();

        public DeleteUserAsyncTests()
        {
            PopulateDB().Wait();
        }

        [Fact]
        public async Task DeleteUserAsync_WithExistingEmail_DeleteUser()
        {
            // Arrange
            var email = "ivan@gmail.com";

            // Act
            await userService.DeleteUserAsync(email);
            var deletedUser = await DbContext.Users.SingleOrDefaultAsync(u => u.Email == email);

            // Assert
            Assert.NotNull(deletedUser);
            Assert.True(deletedUser.IsDeleted);
        }

        [Fact]
        public async Task DeleteUserAsync_WithNonExistingEmail_ThrowsNotFoundException()
        {
            // Arrange
            var email = "invalid@gmail.com";

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => userService.DeleteUserAsync(email));
        }

        [Fact]
        public async Task DeleteUserAsync_WithDeletedUser_ThrowsNotFoundException()
        {
            // Arrange
            var email = "vladi@abv.bg";

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => userService.DeleteUserAsync(email));
        }
    }
}
