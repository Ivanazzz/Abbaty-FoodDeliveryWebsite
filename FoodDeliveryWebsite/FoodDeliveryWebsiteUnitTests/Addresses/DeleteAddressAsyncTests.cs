using FoodDeliveryWebsite.CustomExceptions;
using FoodDeliveryWebsite.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FoodDeliveryWebsite.UnitTests.Addresses
{
    public class DeleteAddressAsyncTests : BaseServiceTests
    {
        private IAddressService addressService => this.ServiceProvider.GetRequiredService<IAddressService>();

        public DeleteAddressAsyncTests()
        {
            PopulateDB().Wait();
        }

        [Fact]
        public async Task DeleteAddressAsync_ValidUserAndAddressId_DeletesAddress()
        {
            // Arrange
            var userEmail = "ivan@gmail.com";
            var addressId = 1; // Existing address id
            int expectedAddressesCount = 0;

            // Act
            await addressService.DeleteAddressAsync(userEmail, addressId);
            var addresses = await addressService.GetAddressesAsync(userEmail);

            // Assert
            Assert.NotNull(addresses);
            Assert.Equal(expectedAddressesCount, addresses.Count);
        }

        [Fact]
        public async Task DeleteAddressAsync_InvalidUser_ThrowsNotFoundException()
        {
            // Arrange
            var nonExistentUserEmail = "nonexistentuser@gmail.com";
            var addressId = 1; // Existing address id

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => addressService.DeleteAddressAsync(nonExistentUserEmail, addressId));
        }

        [Fact]
        public async Task DeleteAddressAsync_InvalidAddress_ThrowsNotFoundException()
        {
            // Arrange
            var userEmail = "ivan@gmail.com";
            var nonExistentAddressId = 999; // Non-existent address id

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => addressService.DeleteAddressAsync(userEmail, nonExistentAddressId));
        }
    }
}
