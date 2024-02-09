using AutoMapper;
using FoodDeliveryWebsite.Controllers;
using FoodDeliveryWebsite.Models.Dtos;
using FoodDeliveryWebsite.Models.Entities;
using FoodDeliveryWebsite.Models;
using FoodDeliveryWebsite.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryWebsite.UnitTests.Addresses.AddressTests
{
    public class AddressAddTest
    {
        private readonly Mock<IAddressRepository> mockRepo;
        private readonly Mock<FoodDeliveryWebsiteDbContext> mockContext;
        private readonly Mock<IMapper> mockMapper;

        public AddressAddTest()
        {
            mockRepo = new Mock<IAddressRepository>();
            mockContext = new Mock<FoodDeliveryWebsiteDbContext>();
            mockMapper = new Mock<IMapper>();
        }

        [Fact]
        public async Task GetAddressesAsync_ReturnsExpectedAddresses()
        {
            // Arrange
            //var userEmail = "test@example.com";
            //var expectedAddresses = AddressTestData.GetTestAddresses();

            

            // Act
            //var result = await addressRepository.GetAddressesAsync(userEmail);

            // Assert
            //Assert.Equal(expectedAddresses.Count, result.Count);
        }
    }
}
