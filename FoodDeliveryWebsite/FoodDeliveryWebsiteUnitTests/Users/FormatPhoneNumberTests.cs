using Microsoft.Extensions.DependencyInjection;

using System.Reflection;

using FoodDeliveryWebsite.Services;

namespace FoodDeliveryWebsite.UnitTests.Users
{
    public class FormatPhoneNumberTests : BaseServiceTests
    {
        private IUserService userService => this.ServiceProvider.GetRequiredService<IUserService>();

        public FormatPhoneNumberTests()
        {
        }

        [Fact]
        public void FormatPhoneNumber_WithValidInput_FormatsCorrectly()
        {
            // Arrange
            var phoneNumber = "+359888888888";
            var expectedFormattedPhoneNumber = "+359 88 8888 888";

            // Act
            var formattedPhoneNumber = InvokePrivateMethod(userService, "FormatPhoneNumber", phoneNumber);

            // Assert
            Assert.Equal(expectedFormattedPhoneNumber, formattedPhoneNumber);
        }

        [Fact]
        public void FormatPhoneNumber_WithInvalidInput_ReturnsOriginalValue()
        {
            // Arrange
            var phoneNumber = "invalidPhoneNumber";
            var expectedFormattedPhoneNumber = "invalidPhoneNumber";

            // Act
            var formattedPhoneNumber = InvokePrivateMethod(userService, "FormatPhoneNumber", phoneNumber);

            // Assert
            Assert.Equal(expectedFormattedPhoneNumber, formattedPhoneNumber);
        }

        private string InvokePrivateMethod(object obj, string methodName, params object[] parameters)
        {
            var methodInfo = obj.GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static);
            if (methodInfo == null)
            {
                throw new ArgumentException($"Method '{methodName}' not found.", nameof(methodName));
            }

            return methodInfo.Invoke(obj, parameters) as string;
        }
    }
}
