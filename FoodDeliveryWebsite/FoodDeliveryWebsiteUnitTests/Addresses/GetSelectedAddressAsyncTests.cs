using Microsoft.Extensions.DependencyInjection;

using FoodDeliveryWebsite.CustomExceptions;
using FoodDeliveryWebsite.Services;

namespace FoodDeliveryWebsite.UnitTests.Addresses
{
    public class GetSelectedAddressAsyncTests : BaseServiceTests
    {
        private IAddressService addressService => ServiceProvider.GetRequiredService<IAddressService>();

        public GetSelectedAddressAsyncTests()
        {
            PopulateDB().Wait();
        }

        [Fact]
        public async Task GetSelectedAddressAsync_ValidInput_ReturnsAddressDto()
        {
            // Arrange
            string userEmail = "ivan@gmail.com";
            int addressId = 1;

            // Act
            var addressDto = await addressService.GetSelectedAddressAsync(userEmail, addressId);

            // Assert
            Assert.NotNull(addressDto);
            Assert.Equal(addressId, addressDto.Id);
            Assert.Equal("София", addressDto.City);
        }

        [Fact]
        public async Task GetSelectedAddressAsync_InvalidUser_ThrowsNotFoundException()
        {
            // Arrange
            string userEmail = "invalid@gmail.com";
            int addressId = 1;

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => addressService.GetSelectedAddressAsync(userEmail, addressId));
        }

        [Fact]
        public async Task GetSelectedAddressAsync_InvalidAddress_ThrowsNotFoundException()
        {
            // Arrange
            string userEmail = "ivan@gmail.com";
            int addressId = 999; // Address ID that does not exist

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => addressService.GetSelectedAddressAsync(userEmail, addressId));
        }

        [Fact]
        public async Task GetSelectedAddressAsync_DeletedAddress_ThrowsNotFoundException()
        {
            // Arrange
            string userEmail = "ivan@gmail.com";
            int addressId = 2; // Address ID of a deleted address

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => addressService.GetSelectedAddressAsync(userEmail, addressId));
        }
    }
}
