using Microsoft.Extensions.DependencyInjection;

using FoodDeliveryWebsite.Services;
using FoodDeliveryWebsite.Models.Dtos.DiscountDtos;

namespace FoodDeliveryWebsite.UnitTests.Discounts
{
    public class GetDiscountAsyncTests : BaseServiceTests
    {
        private IDiscountService discountService => this.ServiceProvider.GetRequiredService<IDiscountService>();

        public GetDiscountAsyncTests()
        {
            PopulateDB().Wait();
        }

        [Fact]
        public async Task GetDiscountAsync_WithValidCode_ReturnsDiscountDto()
        {
            // Arrange
            string validCode = "year2024"; // Assuming this code exists in the database

            // Act
            var discount = await discountService.GetDiscountAsync(validCode);

            // Assert
            Assert.NotNull(discount);
            Assert.IsType<DiscountOrderDto>(discount);
            Assert.Equal(validCode, discount.Code);
        }

        [Fact]
        public async Task GetDiscountAsync_WithInvalidCode_ReturnsEmptyDiscount()
        {
            // Arrange
            string invalidCode = "invalid30";

            // Act
            var discount = await discountService.GetDiscountAsync(invalidCode);

            // Assert
            Assert.Null(discount.Code);
            Assert.Equal(0, discount.Id);
            Assert.Equal(0, discount.Percentage);
        }

        [Fact]
        public async Task GetDiscountAsync_WithUpcomingCode_ReturnsEmptyDiscount()
        {
            // Arrange
            string code = "summer20"; // Assuming this code exists, but the discount is not active yet

            // Act
            var discount = await discountService.GetDiscountAsync(code);

            // Assert
            Assert.Null(discount.Code);
            Assert.Equal(0, discount.Id);
            Assert.Equal(0, discount.Percentage);
        }

        [Fact]
        public async Task GetDiscountAsync_WithExpiredCode_ReturnsEmptyDiscount()
        {
            // Arrange
            string code = "special8"; // Assuming this code exists, but the discount has expired

            // Act
            var discount = await discountService.GetDiscountAsync(code);

            // Assert
            Assert.Null(discount.Code);
            Assert.Equal(0, discount.Id);
            Assert.Equal(0, discount.Percentage);
        }
    }
}
