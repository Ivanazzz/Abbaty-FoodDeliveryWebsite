using Microsoft.Extensions.DependencyInjection;

using FoodDeliveryWebsite.Services;

namespace FoodDeliveryWebsite.UnitTests.Discounts
{
    public class GetAvailableDiscountsAsyncTests : BaseServiceTests
    {
        private IDiscountService discountService => this.ServiceProvider.GetRequiredService<IDiscountService>();

        public GetAvailableDiscountsAsyncTests()
        {
            PopulateDB().Wait();
        }

        [Fact]
        public async Task GetAvailableDiscountsAsync_ReturnsAllAvailableDiscounts()
        {
            // Arrange
            int expectedDiscountsCount = 2;

            // Act
            var availableDiscounts = await discountService.GetAvailableDiscountsAsync();

            // Assert
            Assert.NotNull(availableDiscounts);
            Assert.Equal(expectedDiscountsCount, availableDiscounts.Count);
        }

        [Fact]
        public async Task GetAvailableDiscountsAsync_ReturnsCorrectDiscountDetails()
        {
            // Arrange
            int percerntageMinValue = 0;
            int percerntageMaxValue = 100;

            // Act
            var availableDiscounts = await discountService.GetAvailableDiscountsAsync();

            // Assert
            Assert.All(availableDiscounts, discount =>
            {
                Assert.NotNull(discount.Code);
                Assert.NotNull(discount.StartDate);
                Assert.NotNull(discount.ExpirationDate);
                Assert.InRange(discount.Percentage, percerntageMinValue, percerntageMaxValue);
            });
        }
    }
}
