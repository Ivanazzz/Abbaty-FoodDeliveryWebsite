using Microsoft.Extensions.DependencyInjection;

using FoodDeliveryWebsite.CustomExceptions;
using FoodDeliveryWebsite.Models.Dtos.AddressDtos;
using FoodDeliveryWebsite.Services;

namespace FoodDeliveryWebsite.UnitTests.Addresses
{
    public class AddAddressAsyncTests : BaseServiceTests
    {
        private IAddressService addressService => this.ServiceProvider.GetRequiredService<IAddressService>();

        public AddAddressAsyncTests()
        {
            PopulateDB().Wait();
        }

        [Fact]
        public async Task AddAddressAsync_ValidAddress_ShouldAddSuccessfully()
        {
            // Arrange
            var addressDto = new AddressDto
            {
                City = "Варна",
                Street = "Морска",
                StreetNo = 123,
                Floor = 2,
                ApartmentNo = 5
            };
            int expectedAddressesForUserCount = 2;

            // Act
            var addresses = await addressService.AddAddressAsync("maria@abv.bg", addressDto);

            // Assert
            Assert.NotNull(addresses);
            Assert.Equal(expectedAddressesForUserCount, addresses.Count);
        }

        [Fact]
        public async Task AddAddressAsync_InvalidAddressData_ShouldThrowException()
        {
            // Arrange
            var addressDto = new AddressDto
            {
                // Missing required fields
            };

            // Act
            Task AddingAddress() => addressService.AddAddressAsync("maria@abv.bg", addressDto);

            // Assert
            await Assert.ThrowsAsync<BadRequestException>(AddingAddress);
        }

        [Fact]
        public async Task AddAddressAsync_NonExistingUser_ShouldThrowException()
        {
            // Arrange
            var addressDto = new AddressDto
            {
                City = "Варна",
                Street = "Морска",
                StreetNo = 123,
                Floor = 2,
                ApartmentNo = 5
            };

            // Act
            Task AddingAddress() => addressService.AddAddressAsync("nonexistinguser@gmail.com", addressDto);

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(AddingAddress);
        }
    }
}
