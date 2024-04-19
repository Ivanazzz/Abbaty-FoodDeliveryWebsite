using Microsoft.Extensions.DependencyInjection;

using FoodDeliveryWebsite.CustomExceptions;
using FoodDeliveryWebsite.Models.Dtos.AddressDtos;
using FoodDeliveryWebsite.Services;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryWebsite.UnitTests.Addresses
{
    public class UpdateAddressAsyncTests : BaseServiceTests
    {
        private IAddressService addressService => this.ServiceProvider.GetRequiredService<IAddressService>();

        public UpdateAddressAsyncTests()
        {
            PopulateDB().Wait();
        }

        [Fact]
        public async Task UpdateAddressAsync_ValidData_AddressUpdatedSuccessfully()
        {
            // Arrange
            var userEmail = "ivan@gmail.com"; // Existing user email
            var addressDto = new AddressDto
            {
                Id = 1, // Existing address id
                City = "Варна",
                Street = "Морска",
                StreetNo = 123,
                Floor = 3,
                ApartmentNo = 4
            };

            // Act
            await addressService.UpdateAddressAsync(userEmail, addressDto);
            var updatedAddress =await DbContext.Addresses.SingleOrDefaultAsync(a => a.Id == addressDto.Id);

            // Assert
            Assert.NotNull(updatedAddress);
            Assert.Equal(addressDto.City, updatedAddress.City);
            Assert.Equal(addressDto.Street, updatedAddress.Street);
            Assert.Equal(addressDto.StreetNo, updatedAddress.StreetNo);
            Assert.Equal(addressDto.Floor, updatedAddress.Floor);
            Assert.Equal(addressDto.ApartmentNo, updatedAddress.ApartmentNo);
        }

        [Fact]
        public async Task UpdateAddressAsync_NonExistentUser_ThrowsNotFoundException()
        {
            // Arrange
            var nonExistentUserEmail = "nonexistentuser@gmail.com";
            var addressDto = new AddressDto
            {
                Id = 1, // Existing address id
                City = "Варна",
                Street = "Морска",
                StreetNo = 123,
                Floor = 3,
                ApartmentNo = 4
            };

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => addressService.UpdateAddressAsync(nonExistentUserEmail, addressDto));
        }

        [Fact]
        public async Task UpdateAddressAsync_NonExistentAddress_ThrowsNotFoundException()
        {
            // Arrange
            var userEmail = "ivan@gmail.com"; // Existing user email
            var nonExistentAddressId = 9999; // Non-existent address id
            var addressDto = new AddressDto
            {
                Id = nonExistentAddressId,
                City = "Варна",
                Street = "Морска",
                StreetNo = 123,
                Floor = 3,
                ApartmentNo = 4
            };

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => addressService.UpdateAddressAsync(userEmail, addressDto));
        }
    }
}
