using Microsoft.Extensions.DependencyInjection;

using FoodDeliveryWebsite.CustomExceptions;
using FoodDeliveryWebsite.Models.Dtos.DiscountDtos;
using FoodDeliveryWebsite.Services;

namespace FoodDeliveryWebsite.UnitTests.Discounts
{
    public class AddDiscountAsyncTests : BaseServiceTests
    {
        private IDiscountService discountService => this.ServiceProvider.GetRequiredService<IDiscountService>();

        public AddDiscountAsyncTests()
        {
            PopulateDB().Wait();
        }

        [Fact]
        public async Task AddDiscountAsync_ValidDiscount_ShouldAddSuccessfully()
        {
            // Arrange
            var discountDto = new DiscountDto
            {
                Code = "test15",
                StartDate = DateTime.UtcNow.AddDays(1).ToString(),
                ExpirationDate = DateTime.UtcNow.AddDays(30).ToString(),
                Percentage = 15
            };
            int expectedUpcomingDiscountsCount = 2;

            // Act
            await discountService.AddDiscountAsync(discountDto);
            var discounts = await discountService.GetUpcomingDiscountsAsync();

            // Assert
            Assert.Equal(expectedUpcomingDiscountsCount, discounts.Count);
        }

        [Fact]
        public async Task AddDiscountAsync_MissingStartDate_ShouldThrowBadRequestException()
        {
            // Arrange
            var discountDto = new DiscountDto
            {
                // Missing required fields StartDate
                Code = "test15",
                ExpirationDate = DateTime.UtcNow.AddDays(2).ToString(),
                Percentage = 15
            };

            // Act and Assert
            await Assert.ThrowsAsync<BadRequestException>(() => discountService.AddDiscountAsync(discountDto));
        }

        [Fact]
        public async Task AddDiscountAsync_MissingExpirationDate_ShouldThrowBadRequestException()
        {
            // Arrange
            var discountDto = new DiscountDto
            {
                // Missing required field ExpirationDate
                Code = "test15",
                StartDate = DateTime.UtcNow.AddDays(2).ToString(),
                Percentage = 15
            };

            // Act and Assert
            await Assert.ThrowsAsync<BadRequestException>(() => discountService.AddDiscountAsync(discountDto));
        }

        [Fact]
        public async Task AddDiscountAsync_StartDateGreaterThanExpirationDate_ShouldThrowBadRequestException()
        {
            // Arrange
            var discountDto = new DiscountDto
            {
                Code = "test15",
                StartDate = DateTime.UtcNow.AddDays(2).ToString(),
                ExpirationDate = DateTime.UtcNow.AddDays(1).ToString(), // StartDate > ExpirationDate
                Percentage = 15
            };

            // Act and Assert
            await Assert.ThrowsAsync<BadRequestException>(() => discountService.AddDiscountAsync(discountDto));
        }
    }
}
