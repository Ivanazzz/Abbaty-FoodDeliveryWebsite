using Microsoft.Extensions.DependencyInjection;

using FoodDeliveryWebsite.Services;

namespace FoodDeliveryWebsite.UnitTests.Discounts
{
    public class GetUpcomingDiscountsAsyncTests : BaseServiceTests
    {
        private IDiscountService discountService => this.ServiceProvider.GetRequiredService<IDiscountService>();

        public GetUpcomingDiscountsAsyncTests()
        {
            PopulateDB().Wait();
        }

        [Fact]
        public async Task GetUpcomingDiscountsAsync_ReturnsCorrectNumberOfDiscounts()
        {
            // Act
            var upcomingDiscounts = await discountService.GetUpcomingDiscountsAsync();
            int expectedDiscountsCount = 1;

            // Assert
            Assert.NotNull(upcomingDiscounts);
            Assert.Equal(expectedDiscountsCount, upcomingDiscounts.Count);
        }

        [Fact]
        public async Task GetUpcomingDiscountsAsync_ReturnsCorrectDiscountDetails()
        {
            // Act
            var upcomingDiscounts = await discountService.GetUpcomingDiscountsAsync();
            var firstUpcomingDiscount = upcomingDiscounts.FirstOrDefault();

            // Assert
            Assert.NotNull(firstUpcomingDiscount);
            Assert.Equal("summer20", firstUpcomingDiscount.Code);
        }
    }
}
