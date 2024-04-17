using Microsoft.Extensions.DependencyInjection;

using FoodDeliveryWebsite.CustomExceptions;
using FoodDeliveryWebsite.Models.Dtos.AddressDtos;
using FoodDeliveryWebsite.Services;

namespace FoodDeliveryWebsite.UnitTests.Addresses
{
    public class GetAddressesAsyncTests : BaseServiceTests
    {
        private IAddressService addressService => this.ServiceProvider.GetRequiredService<IAddressService>();

        public GetAddressesAsyncTests()
        {
            PopulateDB().Wait();
        }

        [Fact]
        public async Task GetAddressesAsync_ExistingUser_ReturnsAddresses()
        {
            // Arrange
            string userEmail = "ivan@gmail.com";
            int expectedAddressesForUserCount = 1;

            // Act
            var addresses = await addressService.GetAddressesAsync(userEmail);

            // Assert
            Assert.Equal(expectedAddressesForUserCount, addresses.Count);
        }

        [Fact]
        public async Task GetAddressesAsync_NonExistingUser_ThrowsNotFoundException()
        {
            // Arrange
            string userEmail = "nonexistinguser@gmail.com";

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => addressService.GetAddressesAsync(userEmail));
        }

        [Fact]
        public async Task GetAddressesAsync_DeletedAddress_NotReturned()
        {
            // Arrange
            string userEmail = "ivan@gmail.com";
            int deletedAddressId = 2; // Assuming this address is deleted

            // Act
            var addresses = await addressService.GetAddressesAsync(userEmail);

            // Assert
            Assert.NotNull(addresses);
            Assert.DoesNotContain(addresses, address => address.Id == deletedAddressId);
        }

        [Fact]
        public async Task GetAddressesAsync_CorrectlyMapped()
        {
            // Arrange
            string userEmail = "ivan@gmail.com";

            // Act
            var addresses = await addressService.GetAddressesAsync(userEmail);

            // Assert
            Assert.NotNull(addresses);
            Assert.All(addresses, address =>
            {
                Assert.IsType<AddressDto>(address);
            });
        }
    }
}

